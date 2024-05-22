#include <iostream>
#include <algorithm>
#include <vector>
#include <string>

using namespace std;

struct TLetter
{
    int index;
    int eq1;
    int eq2;
};

int compare(TLetter suf1, TLetter suf2)
{
    if (suf1.eq1 == suf2.eq1) {
        return suf1.eq2 < suf2.eq2;
    }
    return suf1.eq1 < suf2.eq1;
}

string MySubstr(string text, int ind, vector<int>& suffixArr){
    if (ind < suffixArr.size()) {
        string res = text.substr(suffixArr[ind] - 1);
        res.pop_back();
        return res;
    } else {
        return "";
    }
}

string MySubstrWithSize(string text, int ind, int size, vector<int>& suffixArr){
    if (ind < suffixArr.size() && size > 0) {
        string res;
        if ((suffixArr[ind] + size - 2) <= text.size()){
            res = text.substr(suffixArr[ind] - 1, size);
        }
        else{
            res = text.substr(suffixArr[ind] - 1);
            res.pop_back();
        }
        return res;
    } else {
        return "";
    }
}

vector<int> BinSearch (vector<int>& suffixArr, string text, string pattern, int index){
    int l = -1 ;
    int r = suffixArr.size();
    int m;
    string s;
    vector <int> res;
    while (l < r - 1){
        m = (l + r) / 2;
        if (MySubstr(text, m, suffixArr) < pattern){
            l = m;
        }
        else{
            r = m;
        }
    }
    if (r < suffixArr.size() && pattern <= MySubstr(text, r, suffixArr).substr(0, pattern.size())){
        res.clear();
        for (int i = r; i <= suffixArr.size(); i++){
            s = MySubstrWithSize(text, i, pattern.size(), suffixArr);
            if (s == pattern){
                res.push_back(suffixArr[i]);
            }
        }
    }
    return res;
}

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);
    
    string text;
    cin >> text;
    text.push_back('$');
    int n = text.size();
    vector<TLetter> letters(n);
    for (int i = 0; i < n; i++) {
        letters[i].index = i;
        letters[i].eq1 = text[i] - 'a';
        if (i + 1 < n) {
            letters[i].eq2 = text[i + 1] - 'a';
        }
        else {
            letters[i].eq2 = -1;
        }
    }
    sort(letters.begin(), letters.end(), compare);

    vector<int> helpIndex(n);
    for (int k = 4; k < 2 * n; k = k * 2) {
        int curEq = 0;
        int prevEq = letters[0].eq1;
        letters[0].eq1 = curEq;
        helpIndex[letters[0].index] = 0;

        for (int i = 1; i < n; i++) {
            if (letters[i].eq1 == prevEq && letters[i].eq2 == letters[i - 1].eq2) {
                prevEq = letters[i].eq1;
                letters[i].eq1 = curEq;
            }
            else {
                prevEq = letters[i].eq1;
                letters[i].eq1 = ++curEq;
            }
            helpIndex[letters[i].index] = i;
        }

        for (int i = 0; i < n; i++) {
            int newIndex = letters[i].index + k/2;
            if (newIndex<n){
                letters[i].eq2=letters[helpIndex[newIndex]].eq1;
            }
            else {
                letters[i].eq2=-1; 
            }
            sort(letters.begin(), letters.end(), compare); 
        }
    }
    vector<int> suffixArr(n);
    for(int i = 0; i < n; i++) {
        suffixArr[i] = letters[i].index + 1;
    }
    
    int i = 1;
    string pattern;
    while(cin >> pattern){
        vector<int> res;
        res = BinSearch(suffixArr, text, pattern, i);
        if (res.size() != 0){
            cout << i << ": ";
            sort(res.begin(), res.end());
            for (int j = 0; j < res.size(); j++){
                cout << res[j];
                if (j != res.size() - 1){
                    cout << ", ";
                }
            }
            cout << '\n';
        }
        i++;
    }
    return 0; 
}
