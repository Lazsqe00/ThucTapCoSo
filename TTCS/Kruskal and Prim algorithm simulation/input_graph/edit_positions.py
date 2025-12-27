import matplotlib.pyplot as plt
import networkx as nx
import json
import os
import subprocess
from read_graph import load_graph   


if os.path.exists("read_graph.py"):
    subprocess.run(["python", "read_graph.py"])

nodes, edge_list = load_graph("graph.json")

edges = [(e.u, e.v, e.w) for e in edge_list]

G = nx.Graph()
G.add_nodes_from(nodes)
G.add_weighted_edges_from(edges)

if os.path.exists("positions.json"):
    with open("positions.json", "r") as f:
        pos = {int(k): tuple(v) for k, v in json.load(f).items()}
else:
    pos = nx.kamada_kawai_layout(G)

fig, ax = plt.subplots(figsize=(8,6))
nx.draw(G, pos, with_labels=True, node_size=500, node_color='lightblue', edge_color='gray')
nx.draw_networkx_edge_labels(G, pos, edge_labels=nx.get_edge_attributes(G, 'weight'))

selected_node = None

def on_press(event):
    global selected_node
    if event.inaxes != ax: # click ngoai vung do thi
        return
    for n, (x, y) in pos.items():
        if (event.xdata - x)**2 + (event.ydata - y)**2 < 0.01:
            selected_node = n
            break

def on_release(event):
    global selected_node
    selected_node = None

def on_motion(event):
    global selected_node
    if selected_node is None or event.inaxes != ax: return      
    pos[selected_node] = (event.xdata, event.ydata)
    ax.clear()
    nx.draw(G, pos, with_labels=True, node_size=500, node_color='lightblue', edge_color='gray', ax=ax)
    nx.draw_networkx_edge_labels(G, pos, edge_labels=nx.get_edge_attributes(G,'weight'), ax=ax)
    plt.draw()

fig.canvas.mpl_connect('button_press_event', on_press)
fig.canvas.mpl_connect('button_release_event', on_release)
fig.canvas.mpl_connect('motion_notify_event', on_motion)

plt.title("Kéo thả các node để chỉnh vị trí, đóng cửa sổ để lưu.")
plt.show()


pos_to_save = {int(k): tuple(v) for k, v in pos.items()}
with open("positions.json", "w") as f:
    json.dump(pos_to_save, f, indent=4)

print("Đã lưu positions.json!")
