using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CashAccountingShared {
    public static class ExtensionMethods {

        public static string GetPropertyName<T>(this object callerObject, Expression<Func<T>> propertyExpression) {

            if(propertyExpression.Body.NodeType == ExpressionType.MemberAccess) {
                MemberExpression mbrExp = propertyExpression.Body as MemberExpression;
                return mbrExp.Member.Name;
            }
            return string.Empty;
        }

    
    }
}
