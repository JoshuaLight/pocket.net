using System;
using static Pocket.Common.Guard;

namespace Pocket.Common.ObjectTree
{
    public static class ObjectTreeExtensions
    {
        public static Node Tree(this object self, Type type) => 
                      Node.Of(type, self);

        public static Node Tree(this object self)
        {
            Ensure(that: self).NotNull();

            return Node.Of(self.GetType(), self);
        }
    }
}