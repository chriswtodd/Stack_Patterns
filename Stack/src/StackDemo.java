import java.util.function.Supplier;

public class StackDemo {
    public static void main(String[]a){
        var ss1=Stack.<String>empty().push("Hello").push("World").push("!!");
        var ss2=Stack.<String>empty().push("Hello").push("World").push("!!");
        assert ss1==ss2;
        assert ss1.popOrElse(err())==ss2.popOrElse(err());
        assert ss1.popOrElse(err()).popOrElse(err())==Stack.<String>empty().push("Hello");
    }
    public static <T> Supplier<T> err(){return ()->{throw new Error();};}
}
