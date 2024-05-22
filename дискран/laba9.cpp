#include <iostream>
#include <vector>
#include <algorithm>
#include <set>

using namespace std;

void Dfs(const vector<vector<int>>& graph, int v, vector<bool>& used, set<int>& comp) {
    used[v] = true;
    comp.insert(v);
    for (int next : graph[v]) {
        if (!used[next]) {
            Dfs(graph, next, used, comp);
        }
    }
}

vector<set<int>> Solution(const vector<vector<int>>& graph, int n) {
    vector<bool> used(n + 1, false);
    vector<set<int>> components;
    for (int i = 1; i < n + 1; i++) {
        if (!used[i]) {
            set<int> comp;
            Dfs(graph, i, used, comp);
            components.push_back(comp);
        }
    }
    return components;
}

int main()
{
    int n, m;
    cin >> n >> m;
    vector<vector<int>> graph(n + 1);

    int a, b;
    for (int i = 0; i < m; i++){
        cin >> a >> b;
        graph[a].push_back(b);
        graph[b].push_back(a);
    }
    
    vector<set<int>> components = Solution(graph, n);
    
    sort(components.begin(), components.end(), [](const set<int>& c, const set<int>& d) {
        return *c.begin() < *d.begin();
    });

    for (const set<int>& comp : components) {
        for (int v : comp) {
            cout << v << " ";
        }
        cout << '\n';
    }
    return 0;
}

