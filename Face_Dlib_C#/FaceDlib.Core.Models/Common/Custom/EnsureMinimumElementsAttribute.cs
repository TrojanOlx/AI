using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FaceDlib.Core.Models.Custom
{
    public class EnsureMinimumElementsAttribute: ValidationAttribute
    {
        private readonly int _minElements;
        private readonly int _maxElements;
        public EnsureMinimumElementsAttribute(int minElements,int maxElements) {
            _minElements = minElements;
            _maxElements = maxElements;
        }

        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list!=null)
            {
                return list.Count > _minElements&&list.Count<=_maxElements;
            }
            return false;
        }
    }
}
