using System;
using System.Runtime.CompilerServices;
using ConditionalWeakTableExtensions;

namespace Stack {
    public class Stack<T> 
        where T : class
    {
        private delegate R MatchDelegate<R>(Func<R> onEmpty, Func<T, Stack<T>, R> onElem);
        private Stack() {}
        private ConditionalWeakTable<T, Stack<T>> cache = new ConditionalWeakTable<T, Stack<T>>();
        private R Match<R>(MatchDelegate<R> matchDelegate, Func<R> onEmpty, Func<T, Stack<T>, R> onElem) {
            matchDelegate(onEmpty, onElem);
        }
        // public virtual R Match<R>(Func<R> onEmpty, Func<T, Stack<T>, R> onElem) {
        //     //MatchDelegate<R> del = new MatchDelegate<R>(onEmpty);
        // }
        public Stack<T> Push(T elem){return cache.GetValueOrCompute(elem, (elem) => this.NewPush(elem));}
        private Stack<T> NewPush(T elem) {
            Stack<T> self = this;
            self.Match<R>(Empty(), (e,t) = t.Push(e));
            return self;
        }
        private static Stack<T> _empty = new Stack<T>();
        public static Stack<T> Empty() {return _empty;}
        public R Match<R>(Func<R> onEmpty, Func<T,R> onLast, Func<T, Stack<T>, R> onElem) {
            return Match(onEmpty, (e,t) => t.IsEmpty() ? onLast(e):onElem(e,t));
        }
        private string ToStrAux(){return Match(() =>"]",e=>e+"]",(e,t)=>e+"; " + t.ToStrAux());}
        public override string ToString() {return "["+ToStrAux();}
        public bool IsEmpty() {return Match(() => true, (e,t) => false);}
        public T TopOrElse(Func<T> s){return Match(s, (e,t)=>e);}
        public Stack<T> PopOrElse(Func<Stack<T>> s){return Match(s, (e,t)=>t);}
    }
}