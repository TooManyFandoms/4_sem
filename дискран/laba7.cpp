#include <iostream>
#include <stack>

using namespace std;

int main()
{
    int n = 0, m = 0, q = 1;
    cin >> n >> m;
    int w, c;
    int ves[n], price[n];
    for (int i = 0; i < n; i++){
        cin >> w >> c;
        ves[i] = w;
        price[i] = c;
    }
    
    stack<int> things;
    int bag[n + 1][n + 1][m + 1];
    for (int i = 0; i < n + 1; i++){
        for (int j = 0; j < n + 1; j++){
            for (int k = 0; k < m + 1; k++){
                bag[i][j][k] = 0;
            }
        }
    }
    
    for (int i = 1; i < n + 1; i++) {
        for (int k = 1; k < m + 1; k++) {
            if (k - ves[i - 1] >= 0) {
                bag[i][1][k] = max(bag[i - 1][1][k], bag[i - 1][1][k - ves[i - 1]] + price[i - 1]);
            }
            else {
                bag[i][1][k] = bag[i - 1][1][k];
            }
        }
    }
    for (int i = 2; i < n + 1; i++) {
        for (int j = 2; j <= i; j++) {
            for (int k = 1; k < m + 1; k++) {
                if ((bag[i - 1][j - 1][k - ves[i - 1]] != 0) && (k - ves[i - 1] >= 0)) {
                    bag[i][j][k] = max(bag[i - 1][j][k], (bag[i - 1][j - 1][k - ves[i - 1]] + price[i - 1]));
                }
                else {
                    bag[i][j][k] = bag[i - 1][j][k];
                }
            }
        }
    }
    
    int res = 0, a = n, b = 0, k = m;
    for (int cnt = 0; cnt <= n; cnt++) {
        if (bag[a][cnt][k] * cnt > res) {
            res = bag[a][cnt][k] * cnt;
            b = cnt;
        }
    }
    cout << res << '\n';
    
    while (bag[a][b][k] > 0){
        if (bag[a - 1][b][k] == bag[a][b][k]){
            a--;
        }
        else{
            things.push(a);
            b--;
            a--;
            k = k - ves[a];
        }
    }

    while (!things.empty()){
        cout << things.top() << ' ';
        things.pop();
    }

    return 0;
}