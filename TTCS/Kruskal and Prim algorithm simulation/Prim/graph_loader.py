import json
import os
from collections import namedtuple

Edge = namedtuple("Edge", ["u", "v", "w"])

def load_graph(filename="graph.json"):
    with open(filename, "r", encoding="utf-8") as f:
        data = json.load(f)
    
    nodes = data["nodes"]
    edges = [Edge(e["u"], e["v"], e["w"]) for e in data["edges"]]
    return nodes, edges
