﻿//-----------------------------------------------------------------------
// <copyright file="Spring.cs" company="SharpFE">
//     Copyright Iain Sproat, 2012.
// </copyright>
//-----------------------------------------------------------------------

namespace SharpFE
{
    using System;
    using System.Collections.Generic;
    using MathNet.Numerics.LinearAlgebra.Double;
    using MathNet.Numerics.LinearAlgebra.Generic;
    using SharpFE.Stiffness;

    /// <summary>
    /// A spring is a 1D linear element which has a constant stiffness along the local x-axis.
    /// </summary>
    public class ConstantLinearSpring : FiniteElement1D
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantSpring" /> class.
        /// </summary>
        /// <param name="node1">The node at the start of the spring.</param>
        /// <param name="node2">The node at the end of the spring.</param>
        /// <param name="springConstant">The value which defines the constant stiffness of the spring.</param>
        internal ConstantLinearSpring(FiniteElementNode node1, FiniteElementNode node2, double springConstant)
            : base(new LinearElastic1DSpringStiffnessMatrixBuilder(springConstant), node1, node2)
        {
        	// empty
        }
        
        public double SpringConstant
        {
        	get
        	{
        		LinearElastic1DSpringStiffnessMatrixBuilder les = this.StiffnessProvider as LinearElastic1DSpringStiffnessMatrixBuilder;
        		if (les == null)
        		{
        			throw new InvalidOperationException("The expected StiffnessProvider for the ConstantSpring class is that of a LinearElasticSpring.  This does not seem to be the case");
        		}
        		
        		return les.SpringConstant;
        	}
        }
    }
}