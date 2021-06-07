using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class SimpleTreeNode<T>
    {
        public T NodeValue; // значение в узле
        public SimpleTreeNode<T> Parent; // родитель или null для корня
        public List<SimpleTreeNode<T>> Children; // список дочерних узлов или null

        public SimpleTreeNode(T val, SimpleTreeNode<T> parent)
        {
            NodeValue = val;
            Parent = parent;
            Children = new();
        }
    }

    public class SimpleTree<T>
    {
        public SimpleTreeNode<T> Root; // корень, может быть null

        public SimpleTree(SimpleTreeNode<T> root)
        {
            Root = root;
        }

        public void AddChild(SimpleTreeNode<T> ParentNode, SimpleTreeNode<T> NewChild)
        {
            // ваш код добавления нового дочернего узла существующему ParentNode
            NewChild.Parent = ParentNode;
            ParentNode.Children.Add(NewChild);
        }

        public void DeleteNode(SimpleTreeNode<T> NodeToDelete)
        {
            // ваш код удаления существующего узла NodeToDelete
            NodeToDelete.Parent.Children.Remove(NodeToDelete);
        }

        public List<SimpleTreeNode<T>> GetAllNodes()
        {
            // ваш код выдачи всех узлов дерева в определённом порядке
            return GetAllNodeChildren(Root);
        }

        private List<SimpleTreeNode<T>> GetAllNodeChildren(SimpleTreeNode<T> node)
        {
            List<SimpleTreeNode<T>> nodes = new() { node };
            foreach (var child in node.Children)
            {
                nodes.AddRange(GetAllNodeChildren(child));
            }
            return nodes;
        }

        public List<SimpleTreeNode<T>> FindNodesByValue(T val)
        {
            // ваш код поиска узлов по значению
            List<SimpleTreeNode<T>> nodes = new();
            if (Root.NodeValue.Equals(val))
            {
                nodes.Add(Root);
            }
            nodes.AddRange(FindNodesByValueInChildren(Root.Children, val));
            return nodes;
        }

        private List<SimpleTreeNode<T>> FindNodesByValueInChildren(List<SimpleTreeNode<T>> children, T val)
        {
            List<SimpleTreeNode<T>> nodes = new();
            foreach (var child in children)
            {
                if (child.NodeValue.Equals(val))
                {
                    nodes.Add(child);
                }
                nodes.AddRange(FindNodesByValueInChildren(child.Children, val));
            }
            return nodes;
        }

        public void MoveNode(SimpleTreeNode<T> OriginalNode, SimpleTreeNode<T> NewParent)
        {
            // ваш код перемещения узла вместе с его поддеревом -- 
            // в качестве дочернего для узла NewParent
            DeleteNode(OriginalNode);
            AddChild(NewParent, OriginalNode);
        }

        public int Count()
        {
            // количество всех узлов в дереве
            return GetAllNodes().Count;
        }

        public int LeafCount()
        {
            // количество листьев в дереве
            return LeafCountHelper(Root, 0);
        }

        private int LeafCountHelper(SimpleTreeNode<T> node, int count)
        {
            if (node.Children.Count == 0)
            {
                return count + 1;
            }

            foreach (var child in node.Children)
            {
                count = LeafCountHelper(child, count);
            }
            return count;
        }

    }

}