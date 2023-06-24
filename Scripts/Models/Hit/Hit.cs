using Godot;
using SimpleGame.Scripts.Models.CustomNode;

namespace SimpleGame.Scripts.Models.Hit
{
    /// <summary>
    ///     Удар
    /// </summary>
    public class Hit : INode
    {
        public HitData Data { get; protected set; } = new HitData();

        public HitBody Body { get; protected set; }

        public Hit()
        {
            Body = new HitBody(Data);
            
            AddChild(Data.HitBox);

            Data.Collider.ChangeSize(8f, 18);
            Data.HitBox.Collider.ChangeSize(8f,18);
        }
        
        
        
        public void SetPosition(Vector2 pos)
        {
            Body.GlobalPosition = pos;
        }
        
        public void ConnectToNode(Node parent) => parent.AddChild(Body);
        
        public void DisconnectFromNode(Node parent) => parent.RemoveChild(Body);
        
        public void AddChild(Node child) =>  Body.AddChild(child);
        
        public void RemoveChild(Node child) =>  Body.RemoveChild(child);
        
    }
}