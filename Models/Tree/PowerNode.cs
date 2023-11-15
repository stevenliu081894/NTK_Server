namespace NTKServer.Models.Tree
{
    public class NodeState
    {
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }

        public NodeState()
        {
            opened = true;
            disabled = false;
            selected = false;
        }

        public NodeState(bool isSelect)
        {
            opened = true;
            disabled = false;
            selected = isSelect;
        }
    }

    /// <summary>
    /// 權限樹節點
    /// </summary>
    public class PowerNode
    {
        public int id {  get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public NodeState state { get; set; }
        public List<PowerNode> children { get; set; }
    }
}
