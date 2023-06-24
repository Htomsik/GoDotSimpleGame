using Godot;

namespace SimpleGame.Scripts.Models.CustomNode
{
    public interface INode
    {
        void ConnectToNode(Godot.Node parent);

        void DisconnectFromNode(Godot.Node parent);

        void AddChild(Godot.Node child);

        void RemoveChild(Godot.Node child);

        void SetPosition(Vector2 pos);

    }
}