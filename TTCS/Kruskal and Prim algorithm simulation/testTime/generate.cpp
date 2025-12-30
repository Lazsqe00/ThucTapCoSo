#include<bits/stdc++.h>
using namespace std;
using namespace chrono;
using ll = long long;
#define fi first
#define se second
#define FOR(i,a,b) for(int i=a;i<=b;i++)
typedef pair<int,int> ii;

int main(){
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	srand(time(NULL));
	
	while(true){
		int n, m;
		if(!(cin >> n >> m)) break;
		
		ll maxM = 1LL * n * (n - 1) / 2;
		if(m < n - 1 || m > maxM){
			cout << "[!] Khong hop le: N=" << n << ", M=" << m << "\n";
			continue;
		}
		
		string fn = "graph_" + to_string(n) + "_" + to_string(m) + ".txt";
		ofstream fout(fn);
		fout << n << " " << m << "\n";
		
		// ===== B1: Tao cay khung ngau nhien =====
		vector<int> d(n);
		FOR(i, 0, n - 1) d[i] = i + 1;
		random_shuffle(d.begin(), d.end());
		
		set<ii> use;
		
		FOR(i, 1, n - 1){
			int u = d[i];
			int v = d[rand() % i];
			if(u > v) swap(u, v);
			use.insert({u, v});
			int w = rand() % 5000 + 1;
			fout << u << " " << v << " " << w << "\n";
		}
		
		int rem = m - (n - 1);
		if(rem > 0){
			// ===== B2: Tao cac canh con lai =====
			vector<ii> cand;
			FOR(u, 1, n){
				FOR(v, u + 1, n){
					if(!use.count({u, v})) cand.push_back({u, v});
				}
			}
			random_shuffle(cand.begin(), cand.end());
			FOR(i, 0, rem - 1){
				int u = cand[i].first;
				int v = cand[i].second;
				int w = rand() % 5000 + 1;
				fout << u << " " << v << " " << w << "\n";
			}
		}
		
		fout.close();
		cout << "[+] Tao thanh cong: " << fn << "\n";
	}
	
	cout << "end\n";
	return 0;
}
