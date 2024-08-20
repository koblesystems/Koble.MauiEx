using System.ComponentModel;

namespace SampleApp.AutoSuggestBoxSamples;

[Description("Test using control in tab view")]
[SamplePriority(9)]
public partial class TabView : ContentPage
{
    public TabView()
    {
        InitializeComponent();
        Initialize();
    }

    AutoSuggestBox SuggestBox1;
    List<string> countries;
    void Initialize()
    {
        using (var s = typeof(TabView).Assembly.GetManifestResourceStream("SampleApp.Data.Countries.txt"))
            countries = new StreamReader(s).ReadToEnd().Split('\n').Select(t => t.Trim()).ToList();

        SuggestBox2.ItemsSource = countries;
    }

    void SuggestBox_TextChanged(object sender, AutoSuggestBoxTextChangedEventArgs e)
    {
        AutoSuggestBox box = (AutoSuggestBox)sender;
        // Filter the list based on text input

        var suggestions = GetSuggestions(box.Text);
        if (suggestions.Count > 0)
            box.IsSuggestionListOpen = true;

        box.ItemsSource = suggestions;
    }

    List<string> GetSuggestions(string text)
    {
        return string.IsNullOrWhiteSpace(text) ? countries : countries.Where(s => s.StartsWith(text, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        SuggestBox1 = new AutoSuggestBox() { ItemsSource = countries };
        SuggestBox1.TextChanged += SuggestBox_TextChanged;

        uiGrid2.Children.Add(SuggestBox1);
    }
}