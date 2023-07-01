using Godot;

namespace SimpleGame.Scripts.Models.CustomNode
{
    /// <summary>
    ///     Нода
    /// </summary>
    public interface INode
    {
        void ConnectToNode(Node parent);

        void DisconnectFromNode(Node parent);

        void AddChild(Node child);

        void RemoveChild(Node child);

        void SetPosition(Vector2 pos);
    }
}