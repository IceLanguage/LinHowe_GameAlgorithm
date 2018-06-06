namespace TsiU
{
    public class TAny
    {
        public T As<T>() where T : TAny
        {
            return (T)this;
        }
    }
}
