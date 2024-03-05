module Homework2.BinaryTree

type BinaryTreeNode<'a> =
    | Node of 'a * BinaryTreeNode<'a> * BinaryTreeNode<'a>
    | Leaf 

let rec binaryTreeMap mappingFunction =
    function 
    | Leaf -> Leaf
    | Node(node, leftChild, rightChild) ->
        Node(
            mappingFunction node,
            binaryTreeMap mappingFunction leftChild,
            binaryTreeMap mappingFunction rightChild
        )