﻿/*
 * Created by Iain Sproat
 * Date: 25/09/2012
 * Time: 19:41
 * 
 */
using System;
using System.Collections.Generic;
using NUnit.Framework;
using SharpFE;
using MathNet.Numerics.LinearAlgebra.Double;

namespace SharpFE.Tests.Repositories
{
    [TestFixture]
    public class ElementRepositoryTest
    {
        NodeFactory nodeFactory;
        FiniteElementNode node1;
        FiniteElementNode node2;
        FiniteElementNode node3;
        Spring spring1;
        Spring spring2;
        ElementRepository SUT;
        ElementFactory elementFactory;
        
        [SetUp]
        public void Setup()
        {
            nodeFactory = new NodeFactory(ModelType.Truss2D);
            node1 = nodeFactory.Create(0, 0);
            node2 = nodeFactory.Create(0, 1);
            node3 = nodeFactory.Create(0, 2);
            SUT = new ElementRepository();
            elementFactory = new ElementFactory(SUT);
            spring1 = elementFactory.CreateSpring(node1, node2, 1);
            spring2 = elementFactory.CreateSpring(node2, node3, 2);
        }
        
        [Test]
        public void AllElementsConnectedToANodeCanBeFound()
        {
            IList<FiniteElement> results = SUT.GetAllElementsConnectedTo(node1);
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(results.Contains(spring1));
            
            results = SUT.GetAllElementsConnectedTo(node2);
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.Contains(spring1));
            Assert.IsTrue(results.Contains(spring2));
            
            results = SUT.GetAllElementsConnectedTo(node3);
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(results.Contains(spring2));
           
        }
        
        [Test]
        public void AllNodesConnectedViaElementsToANodeCanBeFound()
        {
            IList<FiniteElementNode> results = SUT.GetAllNodesConnectedViaElementsTo(node1);
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(node2, results[0]);
            
            results = SUT.GetAllNodesConnectedViaElementsTo(node2);
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
            Assert.IsTrue(results.Contains(node1));
            Assert.IsTrue(results.Contains(node3));
            
            results = SUT.GetAllNodesConnectedViaElementsTo(node3);
            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(node2, results[0]);
        }
    }
}