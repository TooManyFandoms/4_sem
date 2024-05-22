#include <map>
#include <sstream>
#include <vector>
#include <iostream>
#include <string>
#include <queue>
#include <fstream>

using namespace std;

struct TTrieItem
{
    map<unsigned int, TTrieItem *> outgoingEdges;
    TTrieItem *suffixlink = nullptr;
    vector<int> patternPositions;
};

class TTrie
{
public:
    vector<int> patternCounts;
    TTrie() : root(new TTrieItem), patternSize(0), pieces(0) { root->suffixlink = root; }
    void AddPatternToTrie(const vector<unsigned int> &str)
    {
        TTrieItem *bohr = root, *next;
        for (const unsigned int &s : str)
        {
            if (!(next = GetNextTrieItem(bohr, s)))
            {
                next = new TTrieItem;
                next->suffixlink = root;
                bohr->outgoingEdges.emplace(s, next);
            }
            bohr = next;
        }
        bohr->patternPositions.push_back(pieces++);
    }

    void Search(vector<unsigned int> &text, vector<pair<int, int>> &textCoordinates)
    {
        InitializeTrieWithAhoCorasick();
        TTrieItem *node = root;
        for (int c = 0; c < text.size(); ++c)
        {
            for (int i : node->patternPositions)
            {
                int firstOccurrenceIndex = c - patternCounts[i];
                cout << textCoordinates[firstOccurrenceIndex].first << ", " << textCoordinates[firstOccurrenceIndex].second << ", " << i + 1 << '\n'; // Вывести координаты найденного шаблона в формате <строка>,<столбец>,<позиция шаблона>
            }
            node = FindNextTrieItem(node, text[c]);
        }

        for (int i : node->patternPositions)
        {
            int firstOccurrenceIndex = text.size() - patternCounts[i];
            cout << textCoordinates[firstOccurrenceIndex].first << ", " << textCoordinates[firstOccurrenceIndex].second << ", " << i + 1 << '\n'; // Вывести координаты найденного шаблона в формате <строка>,<столбец>,<позиция шаблона>
        }
    }

    TTrieItem *root;

private:
    int patternSize, pieces;
    TTrieItem *FindNextTrieItem(TTrieItem *item, unsigned int letter)
    {
        try
        {
            item = item->outgoingEdges.at(letter);
        }
        catch (out_of_range &)
        {
            item = (item == root) ? root : FindNextTrieItem(item->suffixlink, letter);
        }
        return item;
    }
    TTrieItem *GetNextTrieItem(TTrieItem *item, unsigned int letter)
    {
        if (!item)
        {
            return nullptr;
        }
        try
        {
            item = item->outgoingEdges.at(letter);
        }
        catch (out_of_range &)
        {
            item = nullptr;
        }
        return item;
    }

    void InitializeTrieWithAhoCorasick()
    {
        queue<TTrieItem *> queue{{root}};

        TTrieItem *node, *child;
        while (!queue.empty())
        {
            TTrieItem *node = queue.front();
            queue.pop();
            map<unsigned int, TTrieItem *>::iterator childPairIt;
            for (childPairIt = node->outgoingEdges.begin(); childPairIt != node->outgoingEdges.end(); ++childPairIt)
            {
                child = childPairIt->second;
                queue.push(child);
                child->suffixlink = Findsuffixlink(child, node, childPairIt->first);
                child->patternPositions.insert(child->patternPositions.end(), child->suffixlink->patternPositions.begin(), child->suffixlink->patternPositions.end());
                child->patternPositions.shrink_to_fit();
            }
        }
    }
    TTrieItem *Findsuffixlink(TTrieItem *child, TTrieItem *parent, const unsigned int letter)
    {
        TTrieItem *linkup = parent->suffixlink, *check;
        while (true)
        {
            check = GetNextTrieItem(linkup, letter);
            if (check)
            {
                return (check != child) ? check : root;
            }
            if (linkup == root)
                return root;
            linkup = linkup->suffixlink;
        }
    }
};

void DeleteTrieItems(TTrieItem *node)
{
    for (auto it = node->outgoingEdges.begin(); it != node->outgoingEdges.end(); ++it)
    {
        TTrieItem *child = it->second;
        DeleteTrieItems(child);
    }
    delete node;
}

int main()
{
    TTrie b;
    int index = 0;
    vector<pair<int, int>> textCoordinates;
    vector<unsigned int> input;
    vector<unsigned int> patterns;
    unsigned int line, word = 0, pattern;

    char c;
    bool flag1 = false, flag2 = false;
    while (true)
    {
        c = getchar();
        if (flag2 && c == '\n')
        {
            break;
        }
        if (!((c < '0') | (c > '9')))
        {
            word = (word << 3) + (word << 1) + (c ^ 48);
            flag1 = true;
            flag2 = false;
        }
        else if (flag1)
        {
            patterns.push_back(word);
            index++;
            word = 0;
            flag1 = false;
        }
        if (c == '\n')
        {
            b.AddPatternToTrie(patterns);
            b.patternCounts.push_back(index);
            index = 0;
            patterns.clear();
            flag2 = true;
        }
    }

    int lineIndex = 1, wordIndex = 1;
    word = 0;
    flag1 = false;
    while ((c = getchar()) != EOF)
    {
        if (!((c < '0') | (c > '9')))
        {
            word = (word << 3) + (word << 1) + (c ^ 48);
            flag1 = true;
        }
        else if (flag1)
        {
            input.push_back(word);
            textCoordinates.push_back(make_pair(lineIndex, wordIndex));
            wordIndex++;
            word = 0;
            flag1 = false;
        }
        if (c == '\n')
        {
            lineIndex++;
            wordIndex = 1;
        }
    }
    if (flag1)
    {
        input.push_back(word);
        textCoordinates.push_back(make_pair(lineIndex, wordIndex));
        wordIndex++;
        word = 0;
        flag1 = false;
    }
    b.Search(input, textCoordinates);
    DeleteTrieItems(b.root);
    return 0;
}