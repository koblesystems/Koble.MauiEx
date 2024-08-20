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

    List<string> countries;
	void Initialize()
    {
        using (var s = typeof(TabView).Assembly.GetManifestResourceStream("SampleApp.Data.Countries.txt"))
            countries = new StreamReader(s).ReadToEnd().Split('\n').Select(t => t.Trim()).ToList();

         SuggestBox1.ItemsSource = countries;
        SuggestBox2.ItemsSource = countries;
    }

	void SuggestBox_TextChanged(object sender, AutoSuggestBoxTextChangedEventArgs e)
    {
        AutoSuggestBox box = (AutoSuggestBox)sender;
        // Filter the list based on text input
        
        var suggestions = GetSuggestions(box.Text);
        if(suggestions.Count > 0)
            box.IsSuggestionListOpen = true;

        box.ItemsSource = suggestions;
    }

    List<string> GetSuggestions(string text)
    {
        return string.IsNullOrWhiteSpace(text) ? countries : countries.Where(s => s.StartsWith(text, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }
}