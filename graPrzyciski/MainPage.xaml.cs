using Microsoft.Maui.Controls;
using System;

namespace graPrzyciski
{
    public partial class MainPage : ContentPage
    {
        Button[,] przyciski;
        StackLayout stackLayout;

        public MainPage()
        {
            InitializeComponent();
            stackLayout = new StackLayout();
        }

        public void GraTworz(object sender, EventArgs e)
        {
            try
            {
                int Plansza = int.Parse(planszaParametry.Text);

                if (Plansza >= 3 && Plansza % 2 == 1)
                {
                    przyciski = new Button[Plansza, Plansza];

                    for (int wie = 0; wie < Plansza; wie++)
                    {
                        var rowLayout = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal
                        };

                        for (int kol = 0; kol < Plansza; kol++)
                        {
                            int tempWIE = wie; // Zmienna tymczasowa dla wie
                            int tempKOL = kol; // Zmienna tymczasowa dla kol

                            przyciski[tempWIE, tempKOL] = new Button
                            {
                                Text = "",
                                WidthRequest = 50,
                                HeightRequest = 50,
                                BackgroundColor = Colors.Red,
                                TextColor = Colors.White,
                                Margin = new Thickness(1)
                            };

                            if (tempWIE == 2 && tempKOL == 2)
                            {
                                przyciski[tempWIE, tempKOL].BackgroundColor = Colors.Green;
                            }

                            przyciski[tempWIE, tempKOL].Clicked += (s, args) => zmianaKoloruPrzycisku(tempWIE, tempKOL);

                            rowLayout.Children.Add(przyciski[tempWIE, tempKOL]);
                        }

                        stackLayout.Children.Add(rowLayout);
                    }
                    Content = new ScrollView { Content = stackLayout };
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                errorHandler.Text = "Podales zla wartosc";
            }
        }

        public void zmianaKoloruPrzycisku(int wie, int kol)
        {
            if (przyciski[wie, kol].BackgroundColor != Colors.Red)
            {
                przyciski[wie, kol].BackgroundColor = Colors.Red;
            }
            else if (przyciski[wie, kol].BackgroundColor != Colors.Green)
            {
                przyciski[wie, kol].BackgroundColor = Colors.Green;
            }

            resztaPrzyciskow(wie - 1, kol);
            resztaPrzyciskow(wie + 1, kol);
            resztaPrzyciskow(wie, kol - 1);
            resztaPrzyciskow(wie, kol + 1);

            warunekWygranej();
        }

        public void resztaPrzyciskow(int wie, int kol)
        {
            if (wie >= 0 && wie < przyciski.GetLength(0) && kol >= 0 && kol < przyciski.GetLength(1))
            {
                if (przyciski[wie, kol].BackgroundColor.Equals(Colors.Green))
                {
                    przyciski[wie, kol].BackgroundColor = Colors.Red;
                }
                else if (przyciski[wie, kol].BackgroundColor.Equals(Colors.Red))
                {
                    przyciski[wie, kol].BackgroundColor = Colors.Green;
                }
            }
        }

        public void warunekWygranej()
        {
            int xoxo = 0;
            for (int wie = 0; wie < przyciski.GetLength(0); wie++)
            {
                for (int kol = 0; kol < przyciski.GetLength(1); kol++)
                {
                    if (przyciski[wie, kol].BackgroundColor.Equals(Colors.Green))
                    {
                        xoxo++;
                    }
                }
            }
            if(xoxo == (przyciski.GetLength(0) * przyciski.GetLength(1)))
            {
                DisplayAlert("GRATULACJE", "Wygrales", "OK");
            }
        }
    }

}