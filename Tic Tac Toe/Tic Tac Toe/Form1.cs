using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {

        bool turn = true;//true=X turn and false=O turn
        int turn_count = 0;
        bool against_computer = false;
        //static String player1, player2;

        public Form1()
        {
            InitializeComponent();
        }
        /*
        public static void setPlayerNames(String n1,String n2)
        {
            player1 = n1;
            player2 = n2;
        }
        */
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By HARIKA", "Tic Tac Toe About");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_click(object sender, EventArgs e)
        {
            if ((p1.Text == "Player1") || (p2.Text == "Player2"))
            {
                MessageBox.Show("You must specify the players' names before you can start!\n Type computer(for Player2) to play against the computer");
            }
            else
            {
                Button b = (Button)sender;
                if (turn)
                    b.Text = "X";
                else
                    b.Text = "O";

                turn = !turn;
                b.Enabled = false;
                turn_count++;

                label2.Focus();
                checkForWinner();

                //Check to see if Playing against computer and it's O's turn
                if ((!turn) && (against_computer))
                {
                    computer_make_move();
                }
            }

        }

        private void checkForWinner()
        {
            bool there_is_a_winner = false;

            //horizontal checks
            if ((a1.Text == a2.Text) && (a2.Text == a3.Text) && (!a1.Enabled))
                there_is_a_winner = true;
            else if ((b1.Text == b2.Text) && (b2.Text == b3.Text) && (!b1.Enabled))
                there_is_a_winner = true;
            else if ((c1.Text == c2.Text) && (c2.Text == c3.Text) && (!c1.Enabled))
                there_is_a_winner = true;

            //vertical checks
            else if ((a1.Text == b1.Text) && (b1.Text == c1.Text) && (!a1.Enabled))
                there_is_a_winner = true;
            else if ((a2.Text == b2.Text) && (b2.Text == c2.Text) && (!a2.Enabled))
                there_is_a_winner = true;
            else if ((a3.Text == b3.Text) && (b3.Text == c3.Text) && (!a3.Enabled))
                there_is_a_winner = true;

            //diagonal checks
            else if ((a1.Text == b2.Text) && (b2.Text == c3.Text) && (!a1.Enabled))
                there_is_a_winner = true;
            else if ((a3.Text == b2.Text) && (b2.Text == c1.Text) && (!c1.Enabled))
                there_is_a_winner = true;

            if (there_is_a_winner)
            {
                disableButtons();
                String winner = "";
                if (turn)
                {
                    winner = p2.Text; ;
                    o_win_count.Text = (Int32.Parse(o_win_count.Text) + 1).ToString();
                }
                else
                {
                    winner = p1.Text;
                    x_win_count.Text = (Int32.Parse(x_win_count.Text) + 1).ToString();
                }
                MessageBox.Show(winner + "   wins", "Yay!");
                newGameToolStripMenuItem.PerformClick();
            }
            else
            {
                if (turn_count == 9)
                {
                    draw_count.Text = (Int32.Parse(draw_count.Text) + 1).ToString();
                    MessageBox.Show("Draw", "Bummer!");
                    newGameToolStripMenuItem.PerformClick();
                }
            }

        }//end of checkForWinner

        private void disableButtons()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }//end foreach
            }//end try
            catch { }

        }

        private void computer_make_move()
        {
            //Priority 1: Get Tic Tac Toe
            //Priority 2: block X tic tac toe
            //Priority 3: Go for corner space
            //Priority 4: Go for open space
            Button move = null;

            //Look for Tic Tac Toe opportunities
            move = look_for_win_or_block("O");//Look for  win
            if(move==null)
            {
                move = look_for_win_or_block("X");//Look for block
                if(move==null)
                {
                    move = look_for_corner();
                    if (move == null)
                    {
                        move = look_for_open_space();
                    }//end if
                }//end if
            }//end if

            move.PerformClick();

        }

        private Button look_for_win_or_block(string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);
            //HORIZONTAL TESTS
            if ((a1.Text == mark) && (a2.Text == mark) && (a3.Text == ""))
                return a3;
            if ((a2.Text == mark) && (a3.Text == mark) && (a1.Text == ""))
                return a1;
            if ((a1.Text == mark) && (a3.Text == mark) && (a2.Text == ""))
                return a2;

            if ((b1.Text == mark) && (b2.Text == mark) && (b3.Text == ""))
                return b3;
            if ((b2.Text == mark) && (b3.Text == mark) && (b1.Text == ""))
                return b1;
            if ((b1.Text == mark) && (b3.Text == mark) && (b2.Text == ""))
                return b2;

            if ((c1.Text == mark) && (c2.Text == mark) && (c3.Text == ""))
                return c3;
            if ((c2.Text == mark) && (c3.Text == mark) && (c1.Text == ""))
                return c1;
            if ((c1.Text == mark) && (c3.Text == mark) && (c2.Text == ""))
                return c2;

            //VERTICAL TESTS
            if ((a1.Text == mark) && (b1.Text == mark) && (c1.Text == ""))
                return c1;
            if ((b1.Text == mark) && (c1.Text == mark) && (a1.Text == ""))
                return a1;
            if ((a1.Text == mark) && (c1.Text == mark) && (b1.Text == ""))
                return b1;

            if ((a2.Text == mark) && (b2.Text == mark) && (c2.Text == ""))
                return c2;
            if ((b2.Text == mark) && (c2.Text == mark) && (a2.Text == ""))
                return a2;
            if ((a2.Text == mark) && (c2.Text == mark) && (b2.Text == ""))
                return b2;

            if ((a3.Text == mark) && (b3.Text == mark) && (c3.Text == ""))
                return c3;
            if ((b3.Text == mark) && (c3.Text == mark) && (a3.Text == ""))
                return a3;
            if ((a3.Text == mark) && (c3.Text == mark) && (b3.Text == ""))
                return b3;

            //DIAGONAL TESTS
            if ((a1.Text == mark) && (b2.Text == mark) && (c3.Text == ""))
                return c3;
            if ((b2.Text == mark) && (c3.Text == mark) && (a1.Text == ""))
                return a1;
            if ((a1.Text == mark) && (c3.Text == mark) && (b2.Text == ""))
                return b2;

            if ((a3.Text == mark) && (b2.Text == mark) && (c1.Text == ""))
                return c1;
            if ((b2.Text == mark) && (c1.Text == mark) && (a3.Text == ""))
                return a3;
            if ((a3.Text == mark) && (c1.Text == mark) && (b2.Text == ""))
                return b2;

            return null;
        }

        private Button look_for_corner()
        {
            Console.WriteLine("Looking for corner");
            if (a1.Text == "O")
            {
                if (a3.Text == "")
                    return a3;
                if (c3.Text == "")
                    return c3;
                if (c1.Text == "")
                    return c1;
            }

            if (a3.Text == "O")
            {
                if (a1.Text == "")
                    return a1;
                if (c3.Text == "")
                    return c3;
                if (c1.Text == "")
                    return c1;
            }

            if (c3.Text == "O")
            {
                if (a1.Text == "")
                    return a1;
                if (a3.Text == "")
                    return a3;
                if (c1.Text == "")
                    return c1;
            }

            if (c1.Text == "O")
            {
                if (a1.Text == "")
                    return a1;
                if (a3.Text == "")
                    return a3;
                if (c3.Text == "")
                    return c3;
            }

            if (a1.Text == "")
                return a1;
            if (a3.Text == "")
                return a3;
            if (c1.Text == "")
                return c1;
            if (c3.Text == "")
                return c3;

            return null;
        }

        private Button look_for_open_space()
        {
            Console.WriteLine("Looking for open space");
            Button b = null;
            foreach (Control c in Controls)
            {
                b = c as Button;//casting done here.If not a button returns null eg: textbox returns null
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }//end if
            }//end if

            return null;
        }


        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turn_count = 0;
    
                foreach (Control c in Controls)
                {
                    try
                    {
                        Button b = (Button)c;
                    b.Enabled = true;
                    b.Text = "";
                    }//end try
                    catch { }
                }//end foreach
            
 }

        private void button_enter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                if (turn)
                    b.Text = "X";
                else
                    b.Text = "O";

            }//end if
        }

        private void button_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {

                b.Text = "";
            }//end if
        }

        private void resetWinCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            o_win_count.Text = "0";
            x_win_count.Text = "0";
            draw_count.Text = "0";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            Form2 f2 = new Form2();
            f2.ShowDialog();
            label1.Text = player1;
            label3.Text = player2;
            */
            setPlayerDefaultsToolStripMenuItem.PerformClick();
        }

        private void p2_TextChanged(object sender, EventArgs e)
        {
            if (p2.Text.ToUpper() == "COMPUTER")
                against_computer = true;
            else
                against_computer = false;
        }

        private void setPlayerDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p1.Text = "Harika";
            p2.Text = "Computer";
        }
    }
}


