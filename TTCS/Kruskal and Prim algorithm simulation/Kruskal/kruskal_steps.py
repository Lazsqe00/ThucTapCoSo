from collections import namedtuple
Edge = namedtuple("Edge", ["u", "v", "w"])

def find(parent, x):
    if parent[x] != x:
        parent[x] = find(parent, parent[x])
    return parent[x]

def union(parent, rank, x, y):
    rootX = find(parent, x)
    rootY = find(parent, y)
    if rootX != rootY:
        if rank[rootX] > rank[rootY]:
            parent[rootY] = rootX
        elif rank[rootX] < rank[rootY]:
            parent[rootX] = rootY
        else:
            parent[rootY] = rootX
            rank[rootX] += 1
        return True
    return False

def mst_steps(nodes, edges):
    
    #B1
    steps = []
    n = len(nodes)
    d = 0  
    sorted_edges = sorted(edges, key=lambda e: e.w)
    
    #B2

    steps.append(("init", [], 1, None, "Khởi tạo MST := empty."))
   
    steps.append(("sort", [], 0, None,
                  f"Sắp xếp các cạnh theo trọng số tăng dần:\n" +
                  ", ".join([f"({e.u}-{e.v}: {e.w})" for e in sorted_edges])))
   
    steps.append(("disjoint_init", [], 2, None, 
    "Khởi tạo DSU xem mỗi đỉnh là một thành phần riêng biệt."))
   
    parent = {i: i for i in nodes}
    rank = {i: 0 for i in nodes}

    #B3
    mst_edges = []
    edge_index = 0
   
    steps.append(("start_loop", [], 3, None, 
    "Bắt đầu duyệt từng cạnh theo thứ tự trọng số tăng dần."))
   
    #B4
    while len(mst_edges) < n - 1 and edge_index < len(sorted_edges):
        e = sorted_edges[edge_index]
        edge_index += 1
        d += 1  #dem so canh da duyet
        u, v, w = e.u, e.v, e.w
       
        steps.append(("consider", list(mst_edges), 4, {u, v},
                      f"Xét cạnh ({u},{v}) trọng số {w}."))
        
        #B5 
        rootU = find(parent, u)
        rootV = find(parent, v)
       
        if rootU == rootV:
            steps.append(("skip", list(mst_edges), 4, {u, v}, f"Bỏ qua cạnh ({u},{v}): {u} và {v} đã cùng thành phần liên thông (sẽ tạo chu trình)."))
        else:
            mst_edges.append(e)
            union(parent, rank, u, v)
           
            steps.append(("add", list(mst_edges), 5, {u, v},
                          f"Thêm cạnh ({u},{v}) trọng số {w} vào MST."))
           
            steps.append(("union", list(mst_edges), 6, None,
                          f"Union thành phần chứa {u} và {v}."))
   
    #B7
    total_weight = sum(e.w for e in mst_edges)
   
    conclusion = (
        "Hoàn tất thuật toán Kruskal.\n"
        f"- Số cạnh đã duyệt: {d}.\n"
        f"- Tổng trọng số của MST: {total_weight}."
    )
   
    steps.append(("done", list(mst_edges), 7, None, conclusion))
   
    return steps