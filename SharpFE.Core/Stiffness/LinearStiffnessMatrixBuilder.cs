﻿

namespace SharpFE.Stiffness
{
	using System;
	using System.Collections.Generic;
	using SharpFE.Elements;
	
	public abstract class LinearTrussStiffnessMatrixBuilder : StiffnessMatrixBuilder
	{
		public LinearTrussStiffnessMatrixBuilder()
		{
			// empty
		}
		
		/// <summary>
		/// Generates the transposed strain-displacement matrix for the given element
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public override KeyedVector<NodalDegreeOfFreedom> GetStrainDisplacementMatrix()
		{
			FiniteElement1D fe1d = this.CastElementToFiniteElement1D();
			
			IList<NodalDegreeOfFreedom> supportedNodalDegreeOfFreedoms = fe1d.SupportedNodalDegreeOfFreedoms;
			KeyedVector<NodalDegreeOfFreedom> B = new KeyedVector<NodalDegreeOfFreedom>(supportedNodalDegreeOfFreedoms);
			
			double originalLength = fe1d.OriginalLength;
			
			B[new NodalDegreeOfFreedom(fe1d.StartNode, DegreeOfFreedom.X)] = -1.0 / originalLength;
			B[new NodalDegreeOfFreedom(fe1d.EndNode, DegreeOfFreedom.X)]   =  1.0 / originalLength;
			
			return B;
		}
		
		protected FiniteElement1D CastElementToFiniteElement1D()
        {        	
        	FiniteElement1D fe1d = this.Element as FiniteElement1D;
			if (fe1d == null)
			{
				throw new NotImplementedException("LinearStiffnessMatrixBuilder has only been implemented for finite elements derived from FiniteElement1D");
			}
			
			return fe1d;
        }
	}
}
