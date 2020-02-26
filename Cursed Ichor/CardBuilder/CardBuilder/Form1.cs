using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CardBuilder
{
    public partial class Form1 : Form
    {

        List<string> spells;
        List<string> fireball;
        List<string> meteor;
        List<string> heal;
        List<string> sacrifice;
        List<string> shock;
        int Count1;
        int Count2;
        int Count3;
        int Count4;
        int Count5;
        int DeckSize;
        StreamWriter writer;
        const int DECKLIMIT = 12;
        
        public Form1()
        {
            spells = new List<string>();
            fireball = new List<string>(); 
            meteor = new List<string>();
            heal = new List<string>();
            sacrifice = new List<string>();
            shock = new List<string>(); 
            InitializeComponent();
        }
        
        //fireball
        private void button1_Click(object sender, EventArgs e) //spell 1 minus
        {
                if (fireball.Contains("Fireball"))
                {
                    fireball.Remove("Fireball");
                    if (fireball.Contains("Fireball") != true)
                    {
                        button1.BackColor = Color.Gray;
                        button1.Enabled = false;
                    }
                TurnOn(button2);
                Count1--;
                DeckSize--;
                TurnAllOn(button2, button4, button6, button8, button10);
                label6.Text = "Count: " + Count1.ToString();
                label11.Text = "Total: " + DeckSize.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e) //spell 1 add
        {
            TurnOn(button1);
            fireball.Add("Fireball");
            Count1++;
            DeckSize++;
            if (Count1 == 3)
            {
                TurnOff(button2);
            }
            if (DeckSize == DECKLIMIT)
                TurnAllOff(button2, button4, button6, button8, button10);
            label6.Text = "Count: " + Count1.ToString();
            label11.Text = "Total: " + DeckSize.ToString();

        }
        
        //heal
        private void button3_Click(object sender, EventArgs e) //spell 2 minus
        {
            if (heal.Contains("Heal"))
            {
                heal.Remove("Heal");
                if (heal.Contains("Heal") != true)
                {
                    button3.BackColor = Color.Gray;
                    button3.Enabled = false;
                }
                TurnOn(button4);
                Count2--;
                DeckSize--;
                TurnAllOn(button2, button4, button6, button8, button10);
                label7.Text = "Count: " + Count2.ToString();
                label11.Text = "Total: " + DeckSize.ToString();

            }

        }

        private void button4_Click(object sender, EventArgs e) //spell 2 add
        {
            TurnOn(button3);
            heal.Add("Heal");
            Count2++;
            DeckSize++;
            if (Count2 == 3)
            {
                TurnOff(button4);
            }
            if (DeckSize == DECKLIMIT)
                TurnAllOff(button2, button4, button6, button8, button10);
            label7.Text = "Count: " + Count2.ToString();
            label11.Text = "Total: " + DeckSize.ToString();

        }

        //meteor
        private void button5_Click(object sender, EventArgs e) //spell 3 minus
        {
            if (meteor.Contains("Meteor"))
            {
                meteor.Remove("Meteor");
                if (meteor.Contains("Meteor") != true)
                {
                    button5.BackColor = Color.Gray;
                    button5.Enabled = false;
                }
                TurnOn(button6);
                Count3--;
                DeckSize--;
                TurnAllOn(button2, button4, button6, button8, button10);
                label8.Text = "Count: " + Count3.ToString();
                label11.Text = "Total: " + DeckSize.ToString();

            }

        }

        private void button6_Click(object sender, EventArgs e) //spell 3 add
        {
            TurnOn(button5);
            meteor.Add("Meteor");
            Count3++;
            DeckSize++;
            if (Count3 == 3)
            {
                TurnOff(button6);
            }
            if (DeckSize == DECKLIMIT)
                TurnAllOff(button2, button4, button6, button8, button10);
            label8.Text = "Count: " + Count3.ToString();
            label11.Text = "Total: " + DeckSize.ToString();

        }

        //shock
        private void button7_Click(object sender, EventArgs e) //spell 4 minus
        {
            if (shock.Contains("Shock"))
            {
                shock.Remove("Shock");
                if (shock.Contains("Shock") != true)
                {
                    button7.BackColor = Color.Gray;
                    button7.Enabled = false;
                }
                TurnOn(button8);
                Count4--;
                DeckSize--;
                TurnAllOn(button2, button4, button6, button8, button10);
                label9.Text = "Count: " + Count4.ToString();
                label11.Text = "Total: " + DeckSize.ToString();

            }
        }

        private void button8_Click(object sender, EventArgs e) //spell 4 add
        {
            TurnOn(button7);
            shock.Add("Shock");
            Count4++;
            DeckSize++;
            if (Count4 == 3)
            {
                TurnOff(button8);
            }
            if (DeckSize == DECKLIMIT)
                TurnAllOff(button2, button4, button6, button8, button10);
            label9.Text = "Count: " + Count4.ToString();
            label11.Text = "Total: " + DeckSize.ToString();

        }

        //sacrifice
        private void button9_Click(object sender, EventArgs e) //spell 5 minus
        {
            if (sacrifice.Contains("Sacrifice"))
            {
                sacrifice.Remove("Sacrifice");
                if (sacrifice.Contains("Sacrifice") != true)
                {
                    button9.BackColor = Color.Gray;
                    button9.Enabled = false;
                }
                TurnOn(button10);
                Count5--;
                DeckSize--;
                TurnAllOn(button2, button4, button6, button8, button10);
                label10.Text = "Count: " + Count5.ToString();
                label11.Text = "Total: " + DeckSize.ToString();

            }

        }

        private void button10_Click(object sender, EventArgs e) //spell 5 add
        {
            TurnOn(button9);
            sacrifice.Add("Sacrifice");
            Count5++;
            DeckSize++;
            if (Count5 == 3)
            {
                TurnOff(button10);
            }
            if (DeckSize == DECKLIMIT)
                TurnAllOff(button2, button4, button6, button8, button10);
            label10.Text = "Count: " + Count5.ToString();
            label11.Text = "Total: " + DeckSize.ToString();

        }
        private void TurnOn(Button button)
        {
            button.Enabled = true;
            button.BackColor = Color.White;
        }
        private void TurnOff(Button button)
        {
            button.Enabled = false;
            button.BackColor = Color.Gray;
        }
        private void TurnAllOff (Button button1, Button button2, Button button3, Button button4, Button button5)
        {
            TurnOff(button1);
            TurnOff(button2);
            TurnOff(button3);
            TurnOff(button4);
            TurnOff(button5);
        }
        private void TurnAllOn(Button button1, Button button2, Button button3, Button button4, Button button5)
        {
            if (Count1 != 3)
                TurnOn(button1);
            if (Count2 != 3)
                TurnOn(button2);
            if (Count3 != 3)
                TurnOn(button3);
            if (Count4 != 3)
                TurnOn(button4);
            if (Count5 != 3)
                TurnOn(button5);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e) //exit
        {
            Environment.Exit(-1);
        }

        private void button11_Click(object sender, EventArgs e) //save
        {
            //adding each card type to the full list of spells
            for(int i = 0; i < fireball.Count; i++)
            {
                spells.Add(fireball[i] + "," + (i + 1));
            }
            for (int i = 0; i < meteor.Count; i++)
            {
                spells.Add(meteor[i] + "," + (i + 1));
            }
            for (int i = 0; i < heal.Count; i++)
            {
                spells.Add(heal[i] + "," + (i + 1));
            }
            for (int i = 0; i < shock.Count; i++)
            {
                spells.Add(shock[i] + "," + (i + 1));
            }
            for (int i = 0; i < sacrifice.Count; i++)
            {
                spells.Add(sacrifice[i] + "," + (i + 1));
            }
            
            //making of the deck text file
            string decksName = null;
            decksName = "..\\..\\..\\..\\Decks\\" + deckName.Text; //text file's name

            writer = new StreamWriter(decksName);

            try
            {
                writer.WriteLine(spells.Count);

                foreach (string s in spells)
                {
                    writer.WriteLine(s);
                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
            }
            
            if (writer != null)
                writer.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void deckName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
