from prim_steps import mst_steps
from graph_loader import load_graph
from draw_prim import draw_step
import networkx as nx
import matplotlib.pyplot as plt
import json

def on_key(event):
    if event.key == 'q':
        plt.close()

def main():
    nodes, edges = load_graph()

    G = nx.Graph()
    G.add_nodes_from(nodes)
    G.add_weighted_edges_from(edges)

    with open("positions.json", "r") as f:
        pos = json.load(f)
        pos = {int(k): tuple(v) for k, v in pos.items()}

    steps = mst_steps(nodes, edges, start_node=0)

    plt.ion()
    fig = plt.figure(figsize=(8, 6))
    fig.canvas.mpl_connect('key_press_event', on_key)

    for step in steps:
        draw_step(G, pos, step, len(nodes))
        if not plt.fignum_exists(fig.number):
            break

    plt.ioff()
    plt.show()

if __name__ == "__main__":
    main()
