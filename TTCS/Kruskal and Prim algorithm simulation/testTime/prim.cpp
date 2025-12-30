#include <bits/stdc++.h>
using namespace std;
using namespace chrono;
using ll = long long;
#define fi first
#define se second
#define FOR(i,a,b) for(int i=a;i<=b;i++)
typedef pair<int,int> ii;

const int MAX = 1e5 + 5;

int N, M;
vector<ii> adj[MAX];
bool vis[MAX];
int key[MAX]; 

ll prim(int s){
	priority_queue<ii, vector<ii>, greater<ii>> pq;
	FOR(i,1,N) key[i] = INT_MAX;
	key[s] = 0;
	pq.push({0, s});
	
	ll mst = 0;
	int cnt = 0;
	int cnt_edge = 0;
	
	while(!pq.empty()){
		int w = pq.top().fi;
		int u = pq.top().se;
		pq.pop();
		cnt_edge++;
		
		if(vis[u]) continue; 
		vis[u] = true;
		mst += w;
		cnt++;
		
		for(auto e : adj[u]){
			int v = e.fi, wv = e.se;
			if(!vis[v] && wv < key[v]){
				key[v] = wv;
				pq.push({wv, v});
			}
		}
	}
	
	cout << "So canh duyet: " << cnt_edge << endl;
	if(cnt < N) return -1; 
	return mst;
}

void reset(){
	FOR(i, 1, N){
		adj[i].clear();
		vis[i] = false;
		key[i] = INT_MAX;
	}
}

int main(){
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	
	vector<string> files = {
		"graph_1000_80000.txt",
		"graph_2000_900000.txt",
		"graph_3000_4498500.txt",
		"graph_4000_7998000.txt",
		"graph_6000_3000000.txt",
		"graph_1000_1200.txt",
		"graph_2000_3000.txt",
		"graph_4000_6000.txt",
		"graph_6000_8000.txt",
		"graph_8000_9000.txt",
	};
	
	for(string fileName : files){
		ifstream fin(fileName);
		if(!fin){
			cout << "Khong mo duoc file " << fileName << "\n";
			continue;
		}
		
		fin >> N >> M;
		reset();
		
		FOR(i, 1, M){
			int u, v, w;
			fin >> u >> v >> w;
			adj[u].push_back({v, w});
			adj[v].push_back({u, w});
		}
		fin.close();
		
		auto st = high_resolution_clock::now();
		ll ans = prim(1);
		auto en = high_resolution_clock::now();
		
		duration<double, milli> tim = en - st;
		
		cout << "File: " << fileName << "\n";
		cout << "N = " << N << ", M = " << M << "\n";
		
		if(ans == -1)
			cout << "Do thi khong lien thong\n";
		else
			cout << "MST Prim = " << ans << "\n";
		
		cout << "Thoi gian: " << tim.count() << " ms\n";
		cout << "--------------------------\n";
	}
	
	return 0;
}
