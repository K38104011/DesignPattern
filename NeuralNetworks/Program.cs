using System.Collections;
using System.Collections.ObjectModel;

var neuron1 = new Neuron();
var neuron2 = new Neuron();

neuron1.ConnectTo(neuron2);
Console.WriteLine(neuron1.Out.FirstOrDefault() == neuron2);
Console.WriteLine(neuron1.In.FirstOrDefault() == null);
Console.WriteLine(neuron2.In.FirstOrDefault() == neuron1);
Console.WriteLine(neuron2.Out.FirstOrDefault() == null);

var layer1 = new NeuronLayer();
var layer2 = new NeuronLayer();

neuron1.ConnectTo(layer1);
layer1.ConnectTo(layer2);

public class Neuron : IEnumerable<Neuron>
{
    public float Value { get; set; }
    public List<Neuron> In = new List<Neuron>(), Out = new List<Neuron>();

    public IEnumerator<Neuron> GetEnumerator()
    {
        yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class NeuronLayer : Collection<Neuron>
{

}

public static class ExtensionMethods
{
    public static void ConnectTo(this IEnumerable<Neuron> self,
    IEnumerable<Neuron> other)
    {
        if (ReferenceEquals(self, other)) return;

        foreach (var from in self)
        {
            foreach (var to in other)
            {
                from.Out.Add(to);
                to.In.Add(from);
            }
        }

    }
}
