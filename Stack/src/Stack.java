import java.util.WeakHashMap;
import java.util.function.BiFunction;
import java.util.function.Function;
import java.util.function.Supplier;

public class Stack<T>{
    private Stack(){} //Private constructor
    private WeakHashMap<T, Stack<T>> cache = new WeakHashMap<>();//one for each stack object
    public <R> R match(Supplier<R> onEmpty, BiFunction<T,Stack<T>,R> onElem){
        return onEmpty.get();
    }
    public Stack<T> push(T elem){return cache.computeIfAbsent(elem,this::newPush);}
    private Stack<T> newPush(T elem){//same implementation as before, but in a private method
        Stack<T> self=this;//explicit this naming
        return new Stack<T>(){//closure
            public <R> R match(Supplier<R> onEmpty, BiFunction<T,Stack<T>,R> onElem){
                return onElem.apply(elem,self);
            }};}
    private static Stack<Object> empty=new Stack<Object>();//singleton pattern
    @SuppressWarnings("unchecked")public static <T> Stack<T> empty(){return (Stack<T>)empty;}
    public <R> R match(Supplier<R> onEmpty, Function<T,R> onLast, BiFunction<T,Stack<T>,R> onElem){
        return match(onEmpty,(e,t)->t.isEmpty()?onLast.apply(e):onElem.apply(e,t));
    }
    private String toStrAux(){return match(()->"]",e->e+"]",(e,t)->e+"; "+t.toStrAux());}
    public String toString(){return "["+toStrAux();}
    public boolean isEmpty(){return match(()->true,(e,t)->false);}
    public T topOrElse(Supplier<T> s){return match(s,(e, t)->e);}
    public Stack<T> popOrElse(Supplier<Stack<T>> s){return match(s,(e,t)->t);}
    //note: no need of equals/hashcode any more!! The identity is now the right one!
}
