#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

struct TSegment {
    int left;
    int right;
    int number;
};

bool comparator1(const TSegment& seg1, const TSegment& seg2) {
    return (seg1.left < seg2.left) || ((seg1.left == seg2.left) && (seg1.right < seg2.right));
}

bool comparator2(const TSegment& res1, const TSegment& res2) {
    return res1.number < res2.number;
}

int main() {
    int n, m;
    cin >> n;
    vector<TSegment> seg;
    TSegment a;
    for (int i = 0; i < n; i++) {
        cin >> a.left >> a.right;
        a.number = i;
        seg.push_back(a);
    }
    cin >> m;
    sort(seg.begin(), seg.end(), comparator1);
    vector<TSegment> res;
    int leftEnd = 0, rightEnd = 0;
    TSegment currSeg;
    for (int i = 0; i < seg.size(); i++) {
        std::cout << "0) i=" << i << std::endl;
        while (seg[i].right <= leftEnd) {
            i++;

        }
        if (i == seg.size()) {
            break;
        }
        currSeg = seg[i];
        while ((seg[i].left <= leftEnd) && (i < seg.size())) {
            if (seg[i].right >= rightEnd) {
                rightEnd = seg[i].right;
                currSeg = seg[i];
            }
            i++;
        }
        res.push_back(currSeg);
        leftEnd = rightEnd;
        if (rightEnd >= m) {
            break;
        }
        if (seg[i].left <= leftEnd) {
            i--;
        }
    }
    if (rightEnd >= m) {
        cout << res.size() << '\n';
        sort(res.begin(), res.end(), comparator2);
        for (int i = 0; i < res.size(); i++) {
            cout << res[i].left << ' ' << res[i].right << '\n';
        }
    } else {
        cout << '0' << '\n';
    }
    return 0;
}

