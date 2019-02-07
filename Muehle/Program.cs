using System;

namespace Muehle
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Deklaration von Variablen

            string[] Kreuzung = {
                                  "A1", "A4", "B2", "B4", "B6", "C3", "C4", "C5", "D1", "D2", "D3", "D5", "D6", "D7",
                                  "E3", "E4", "E5", "F2", "F4", "F6", "G1", "G4", "G7", "A7"
                                };

            bool Spieler = false;
            bool Computer = false;
            bool SteinNehmen = false;

            int SpielerSteine = 0;
            int MaxSpielerSteine = 9;
            int ComputerSteine = 0;
            int MaxComputerSteine = 9;

            #endregion

            // Vorab
            Ausgabe_Anleitung();
            Definiere_Anfang(ref Spieler, ref Computer);

            // PHASE 1
            Beginne_Phase_1();
            Zeichne_Spielfeld(ref Kreuzung);
            Spiele_Phase_1
            (
                ref SpielerSteine, ref MaxSpielerSteine, ref ComputerSteine, ref MaxComputerSteine, ref Kreuzung, ref Spieler, 
                ref Computer, ref SteinNehmen
            );

            // PHASE 2
            Beginne_Phase_2
            (
                ref SpielerSteine, ref MaxSpielerSteine, ref ComputerSteine, ref MaxComputerSteine, ref Kreuzung
            );
            Spiele_Phase_2
            (
                ref SpielerSteine, ref MaxSpielerSteine, ref ComputerSteine, ref MaxComputerSteine, ref Kreuzung, ref Spieler, 
                ref Computer, ref SteinNehmen
            );

            // PHASE 3
            Beginne_Phase_3(ref MaxSpielerSteine, ref MaxComputerSteine);

        }

        #region Ausgabe_Anleitung

        static void Ausgabe_Anleitung()
        {
            string Anleitung = "\nSorge dafür, dass dein Gegner weniger als 3 Steine übrig hat oder dass er nicht mehr ziehen kann." +
                   "\nDas Spiel besteht aus 3 Phasen: Einsetzen, Versetzen und Endspiel." +
                   "\n\nIn jeder Phase kannst du Steine deines Gegners vom Brett entfernen, wenn du 3 Steine in eine Reihe setzt (horizontal" +
                   "\noder vertikal) - diese drei Steine werden auch Mühle genannt. Jedes Mal, wenn du eine Mühle bildest, musst du einen" +
                   "\nStein deines Gegners entfernen, wenn nicht alle seine Steine Teile von seinen Mühlen sind - diese sind geschützt." +
                   "\nAllerdings nur solange der andere Spieler mehr als 3 Steine hat!" +
                   "\n\n\nQuelle: brettspielnetz.de" +
                   "\n\n\n\n\nDrücke eine beliebige Taste, um das Spiel zu starten...";

            Console.WindowHeight = 30;
            Console.WindowWidth = 117;
            Console.Title = "Mühle - Ziel und Grundlage des Spiels";
            Console.Write(Anleitung);
            Console.ReadKey();
            Console.Clear();
        }

        #endregion

        #region Definiere_Anfang

        static void Definiere_Anfang(ref bool Spieler, ref bool Computer)
        {
            string Eingabe = "";

            Console.Write
            (
                "Wer soll anfangen?" +
                " \n\n\nKI oder SP: "
            );
            Eingabe = Console.ReadLine().ToUpper();

            if (Eingabe == "KI")
                Computer = true;
            else if (Eingabe == "SP")
                Spieler = true;
            else
                Computer = true;
        }

        #endregion

        #region PHASE 1

        #region Beginne_Phase_1

        static void Beginne_Phase_1()
        {
            Console.WindowWidth = 45;
            Console.Title = "Mühle - PHASE 1: Einsetzen";
        }

        #endregion

        #region Zeichne_Spielfeld
        static void Zeichne_Spielfeld(ref string[] Kreuzung)
        {
            string Spielfeld = "\n" +
                   " 7     {23}--------------{13}--------------{22}\n" +
                   "       |                                |\n" +
                   " 6     |  {4}-----------{12}-----------{19}  |\n" +
                   "       |  |                          |  |\n" +
                   " 5     |  |  {7}--------{11}--------{16}  |  |\n" +
                   "       |  |  |                    |  |  |\n" +
                   " 4     {1}-{3}-{6}                  {15}-{18}-{21}\n" +
                   "       |  |  |                    |  |  |\n" +
                   " 3     |  |  {5}--------{10}--------{14}  |  |\n" +
                   "       |  |                          |  |\n" +
                   " 2     |  {2}-----------{9}-----------{17}  |\n" +
                   "       |                                |\n" +
                   " 1     {0}--------------{8}--------------{20}\n\n\n" +
                   "       A  B  C         D         E  F  G\n\n";

            Console.WriteLine
            (
                Spielfeld, Kreuzung[0], Kreuzung[1], Kreuzung[2], Kreuzung[3], Kreuzung[4],
                Kreuzung[5], Kreuzung[6], Kreuzung[7], Kreuzung[8], Kreuzung[9], Kreuzung[10], Kreuzung[11],
                Kreuzung[12], Kreuzung[13], Kreuzung[14], Kreuzung[15], Kreuzung[16], Kreuzung[17],
                Kreuzung[18], Kreuzung[19], Kreuzung[20], Kreuzung[21], Kreuzung[22], Kreuzung[23]
            );
        }
        #endregion

        #region Spiele_Phase_1

        static void Spiele_Phase_1
        (
            ref int SpielerSteine, ref int MaxSpielerSteine, ref int ComputerSteine, ref int MaxComputerSteine, ref string[] Kreuzung, ref bool Spieler, ref bool Computer, ref bool SteinNehmen
        )
        {
            while (SpielerSteine < MaxSpielerSteine || ComputerSteine < MaxComputerSteine)
            {
                Setze_Spieler_Stein
                (
                    ref SpielerSteine, ref Kreuzung, ref Spieler, ref Computer, ref SteinNehmen
                );

                Setze_Computer_Stein
                (
                    ref ComputerSteine, ref Kreuzung, ref Spieler, ref Computer, ref SteinNehmen
                );
            }
        }

        #endregion

        #region Setze_Spieler_Stein

        static void Setze_Spieler_Stein
        (
            ref int SpielerSteine, ref string[] Kreuzung, ref bool Spieler, ref bool Computer, ref bool SteinNehmen
        )
        {
            string Eingabe = "";

            while (Spieler)
            {
                Console.Write("Gib' die Koordinate ein: ");
                Eingabe = Console.ReadLine().ToUpper();

                if (Eingabe == Kreuzung[0])
                {
                    if (Kreuzung[0] != "SP" || Kreuzung[0] != "KI")
                    {
                        Kreuzung[0] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[1])
                {
                    if (Kreuzung[1] != "SP" || Kreuzung[1] != "KI")
                    {
                        Kreuzung[1] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[2])
                {
                    if (Kreuzung[2] != "SP" || Kreuzung[2] != "KI")
                    {
                        Kreuzung[2] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[3])
                {
                    if (Kreuzung[3] != "SP" || Kreuzung[3] != "KI")
                    {
                        Kreuzung[3] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[4])
                {
                    if (Kreuzung[4] != "SP" || Kreuzung[4] != "KI")
                    {
                        Kreuzung[4] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[5])
                {
                    if (Kreuzung[5] != "SP" || Kreuzung[5] != "KI")
                    {
                        Kreuzung[5] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[6])
                {
                    if (Kreuzung[6] != "SP" || Kreuzung[6] != "KI")
                    {
                        Kreuzung[6] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[7])
                {
                    if (Kreuzung[7] != "SP" || Kreuzung[7] != "KI")
                    {
                        Kreuzung[7] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[8])
                {
                    if (Kreuzung[8] != "SP" || Kreuzung[8] != "KI")
                    {
                        Kreuzung[8] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[9])
                {
                    if (Kreuzung[9] != "SP" || Kreuzung[9] != "KI")
                    {
                        Kreuzung[9] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[10])
                {
                    if (Kreuzung[10] != "SP" || Kreuzung[10] != "KI")
                    {
                        Kreuzung[10] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[11])
                {
                    if (Kreuzung[11] != "SP" || Kreuzung[11] != "KI")
                    {
                        Kreuzung[11] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[12])
                {
                    if (Kreuzung[12] != "SP" || Kreuzung[12] != "KI")
                    {
                        Kreuzung[12] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[13])
                {
                    if (Kreuzung[13] != "SP" || Kreuzung[13] != "KI")
                    {
                        Kreuzung[13] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[14])
                {
                    if (Kreuzung[14] != "SP" || Kreuzung[14] != "KI")
                    {
                        Kreuzung[14] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[15])
                {
                    if (Kreuzung[15] != "SP" || Kreuzung[15] != "KI")
                    {
                        Kreuzung[15] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[16])
                {
                    if (Kreuzung[16] != "SP" || Kreuzung[16] != "KI")
                    {
                        Kreuzung[16] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[17])
                {
                    if (Kreuzung[17] != "SP" || Kreuzung[17] != "KI")
                    {
                        Kreuzung[17] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[18])
                {
                    if (Kreuzung[18] != "SP" || Kreuzung[18] != "KI")
                    {
                        Kreuzung[18] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[19])
                {
                    if (Kreuzung[19] != "SP" || Kreuzung[19] != "KI")
                    {
                        Kreuzung[19] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[20])
                {
                    if (Kreuzung[20] != "SP" || Kreuzung[20] != "KI")
                    {
                        Kreuzung[20] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[21])
                {
                    if (Kreuzung[21] != "SP" || Kreuzung[21] != "KI")
                    {
                        Kreuzung[21] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[22])
                {
                    if (Kreuzung[22] != "SP" || Kreuzung[22] != "KI")
                    {
                        Kreuzung[22] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }
                else if (Eingabe == Kreuzung[23])
                {
                    if (Kreuzung[23] != "SP" || Kreuzung[23] != "KI")
                    {
                        Kreuzung[23] = "SP";
                        SpielerSteine++;

                        Console.Clear();

                        Computer = true;
                        Spieler = false;
                    }
                }

                Zeichne_Spielfeld(ref Kreuzung);

                Nimm_KI_Stein_Phase_1(ref Eingabe, ref Kreuzung, ref SteinNehmen);
            }
        }

        #endregion
        #region Nimm_KI_Stein_Phase_1

        static void Nimm_KI_Stein_Phase_1(ref string Eingabe, ref string[] Kreuzung, ref bool SteinNehmen)
        {
            if (Kreuzung[23] == "SP" && Kreuzung[13] == "SP" && Kreuzung[22] == "SP" ||
                Kreuzung[23] == "SP" && Kreuzung[1] == "SP" && Kreuzung[0] == "SP" ||
                Kreuzung[20] == "SP" && Kreuzung[21] == "SP" && Kreuzung[22] == "SP" ||
                Kreuzung[20] == "SP" && Kreuzung[8] == "SP" && Kreuzung[0] == "SP" ||
                Kreuzung[1] == "SP" && Kreuzung[3] == "SP" && Kreuzung[6] == "SP" ||
                Kreuzung[15] == "SP" && Kreuzung[18] == "SP" && Kreuzung[21] == "SP" ||
                Kreuzung[4] == "SP" && Kreuzung[3] == "SP" && Kreuzung[2] == "SP" ||
                Kreuzung[4] == "SP" && Kreuzung[12] == "SP" && Kreuzung[19] == "SP" ||
                Kreuzung[17] == "SP" && Kreuzung[18] == "SP" && Kreuzung[19] == "SP" ||
                Kreuzung[17] == "SP" && Kreuzung[9] == "SP" && Kreuzung[2] == "SP" ||
                Kreuzung[7] == "SP" && Kreuzung[6] == "SP" && Kreuzung[5] == "SP" ||
                Kreuzung[7] == "SP" && Kreuzung[11] == "SP" && Kreuzung[16] == "SP" ||
                Kreuzung[14] == "SP" && Kreuzung[15] == "SP" && Kreuzung[16] == "SP" ||
                Kreuzung[14] == "SP" && Kreuzung[10] == "SP" && Kreuzung[5] == "SP")
            {
                SteinNehmen = true;
            }

            while (SteinNehmen)
            {
                Console.Write("Such' dir ein KI-Stein aus: ");
                Eingabe = Console.ReadLine().ToUpper();

                if (Eingabe == "A1")
                {
                    if (Kreuzung[0] == "KI")
                    {
                        Kreuzung[0] = "A1";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "A4")
                {
                    if (Kreuzung[1] == "KI")
                    {
                        Kreuzung[1] = "A4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "A7")
                {
                    if (Kreuzung[23] == "KI")
                    {
                        Kreuzung[23] = "A7";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "B2")
                {
                    if (Kreuzung[2] == "KI")
                    {
                        Kreuzung[2] = "B2";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "B4")
                {
                    if (Kreuzung[3] == "KI")
                    {
                        Kreuzung[3] = "A4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "B6")
                {
                    if (Kreuzung[4] == "KI")
                    {
                        Kreuzung[4] = "A4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "C3")
                {
                    if (Kreuzung[5] == "KI")
                    {
                        Kreuzung[5] = "C3";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "C4")
                {
                    if (Kreuzung[6] == "KI")
                    {
                        Kreuzung[6] = "C4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "C5")
                {
                    if (Kreuzung[7] == "KI")
                    {
                        Kreuzung[7] = "C5";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "D1")
                {
                    if (Kreuzung[8] == "KI")
                    {
                        Kreuzung[8] = "D1";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "D2")
                {
                    if (Kreuzung[9] == "KI")
                    {
                        Kreuzung[9] = "D2";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "D3")
                {
                    if (Kreuzung[10] == "KI")
                    {
                        Kreuzung[10] = "D3";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "D5")
                {
                    if (Kreuzung[11] == "KI")
                    {
                        Kreuzung[11] = "D5";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "D6")
                {
                    if (Kreuzung[12] == "KI")
                    {
                        Kreuzung[12] = "D6";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "D7")
                {
                    if (Kreuzung[13] == "KI")
                    {
                        Kreuzung[13] = "D7";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "E3")
                {
                    if (Kreuzung[14] == "KI")
                    {
                        Kreuzung[14] = "E3";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "E4")
                {
                    if (Kreuzung[15] == "KI")
                    {
                        Kreuzung[15] = "E4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "E5")
                {
                    if (Kreuzung[16] == "KI")
                    {
                        Kreuzung[16] = "E5";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "F2")
                {
                    if (Kreuzung[17] == "KI")
                    {
                        Kreuzung[17] = "F2";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "F4")
                {
                    if (Kreuzung[18] == "KI")
                    {
                        Kreuzung[18] = "F4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "F6")
                {
                    if (Kreuzung[19] == "KI")
                    {
                        Kreuzung[19] = "F6";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "G1")
                {
                    if (Kreuzung[20] == "KI")
                    {
                        Kreuzung[20] = "G1";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "G4")
                {
                    if (Kreuzung[21] == "KI")
                    {
                        Kreuzung[21] = "G4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "G7")
                {
                    if (Kreuzung[22] == "KI")
                    {
                        Kreuzung[22] = "G7";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }

                Zeichne_Spielfeld(ref Kreuzung);
            }
        }

        #endregion

        #region Setze_Computer_Stein

        static void Setze_Computer_Stein
        (
            ref int ComputerSteine, ref string[] Kreuzung, ref bool Spieler, ref bool Computer, ref bool SteinNehmen
        )
        {
            Random Zufall = new Random();
            string KI;

            while (Computer)
            {
                KI = Kreuzung[Zufall.Next(0, Kreuzung.Length)];

                if (KI == Kreuzung[0])
                {
                    if (Kreuzung[0] != "SP" || Kreuzung[0] != "KI")
                    {
                        Kreuzung[0] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[1])
                {
                    if (Kreuzung[1] != "SP" || Kreuzung[1] != "KI")
                    {
                        Kreuzung[1] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[2])
                {
                    if (Kreuzung[2] != "SP" || Kreuzung[2] != "KI")
                    {
                        Kreuzung[2] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[3])
                {
                    if (Kreuzung[3] != "SP" || Kreuzung[3] != "KI")
                    {
                        Kreuzung[3] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[4])
                {
                    if (Kreuzung[4] != "SP" || Kreuzung[4] != "KI")
                    {
                        Kreuzung[4] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[5])
                {
                    if (Kreuzung[5] != "SP" || Kreuzung[5] != "KI")
                    {
                        Kreuzung[5] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[6])
                {
                    if (Kreuzung[6] != "SP" || Kreuzung[6] != "KI")
                    {
                        Kreuzung[6] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[7])
                {
                    if (Kreuzung[7] != "SP" || Kreuzung[7] != "KI")
                    {
                        Kreuzung[7] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[8])
                {
                    if (Kreuzung[8] != "SP" || Kreuzung[8] != "KI")
                    {
                        Kreuzung[8] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[9])
                {
                    if (Kreuzung[9] != "SP" || Kreuzung[9] != "KI")
                    {
                        Kreuzung[9] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[10])
                {
                    if (Kreuzung[10] != "SP" || Kreuzung[10] != "KI")
                    {
                        Kreuzung[10] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[11])
                {
                    if (Kreuzung[11] != "SP" || Kreuzung[11] != "KI")
                    {
                        Kreuzung[11] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[12])
                {
                    if (Kreuzung[12] != "SP" || Kreuzung[12] != "KI")
                    {
                        Kreuzung[12] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[13])
                {
                    if (Kreuzung[13] != "SP" || Kreuzung[13] != "KI")
                    {
                        Kreuzung[13] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[14])
                {
                    if (Kreuzung[14] != "SP" || Kreuzung[14] != "KI")
                    {
                        Kreuzung[14] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[15])
                {
                    if (Kreuzung[15] != "SP" || Kreuzung[15] != "KI")
                    {
                        Kreuzung[15] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[16])
                {
                    if (Kreuzung[16] != "SP" || Kreuzung[16] != "KI")
                    {
                        Kreuzung[16] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[17])
                {
                    if (Kreuzung[17] != "SP" || Kreuzung[17] != "KI")
                    {
                        Kreuzung[17] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[18])
                {
                    if (Kreuzung[18] != "SP" || Kreuzung[18] != "KI")
                    {
                        Kreuzung[18] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[19])
                {
                    if (Kreuzung[19] != "SP" || Kreuzung[19] != "KI")
                    {
                        Kreuzung[19] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[20])
                {
                    if (Kreuzung[20] != "SP" || Kreuzung[20] != "KI")
                    {
                        Kreuzung[20] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[21])
                {
                    if (Kreuzung[21] != "SP" || Kreuzung[21] != "KI")
                    {
                        Kreuzung[21] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[22])
                {
                    if (Kreuzung[22] != "SP" || Kreuzung[22] != "KI")
                    {
                        Kreuzung[22] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }
                else if (KI == Kreuzung[23])
                {
                    if (Kreuzung[23] != "SP" || Kreuzung[23] != "KI")
                    {
                        Kreuzung[23] = "KI";
                        ComputerSteine++;

                        Console.Clear();

                        Computer = false;
                        Spieler = true;
                    }
                }

                Zeichne_Spielfeld(ref Kreuzung);

                Nimm_Spieler_Stein_Phase_1(ref Kreuzung, ref SteinNehmen);

            }
        }

        #endregion
        #region Nimm_Spieler_Stein_Phase_1

        static void Nimm_Spieler_Stein_Phase_1(ref string[] Kreuzung, ref bool SteinNehmen)
        {
            Random Zufall = new Random();
            string KI;

            if (Kreuzung[23] == "KI" && Kreuzung[13] == "KI" && Kreuzung[22] == "KI" ||
                Kreuzung[23] == "KI" && Kreuzung[1] == "KI" && Kreuzung[0] == "KI" ||
                Kreuzung[20] == "KI" && Kreuzung[21] == "KI" && Kreuzung[22] == "KI" ||
                Kreuzung[20] == "KI" && Kreuzung[8] == "KI" && Kreuzung[0] == "KI" ||
                Kreuzung[1] == "KI" && Kreuzung[3] == "KI" && Kreuzung[6] == "KI" ||
                Kreuzung[15] == "KI" && Kreuzung[18] == "KI" && Kreuzung[21] == "KI" ||
                Kreuzung[4] == "KI" && Kreuzung[3] == "KI" && Kreuzung[2] == "KI" ||
                Kreuzung[4] == "KI" && Kreuzung[12] == "KI" && Kreuzung[19] == "KI" ||
                Kreuzung[17] == "KI" && Kreuzung[18] == "KI" && Kreuzung[19] == "KI" ||
                Kreuzung[17] == "KI" && Kreuzung[9] == "KI" && Kreuzung[2] == "KI" ||
                Kreuzung[7] == "KI" && Kreuzung[6] == "KI" && Kreuzung[5] == "KI" ||
                Kreuzung[7] == "KI" && Kreuzung[11] == "KI" && Kreuzung[16] == "KI" ||
                Kreuzung[14] == "KI" && Kreuzung[15] == "KI" && Kreuzung[16] == "KI" ||
                Kreuzung[14] == "KI" && Kreuzung[10] == "KI" && Kreuzung[5] == "KI")
            {
                SteinNehmen = true;
            }

            while (SteinNehmen)
            {
                KI = Kreuzung[Zufall.Next(0, Kreuzung.Length)];

                if (KI == "A1")
                {
                    if (Kreuzung[0] == "SP")
                    {
                        Kreuzung[0] = "A1";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "A4")
                {
                    if (Kreuzung[1] == "SP")
                    {
                        Kreuzung[1] = "A4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "A7")
                {
                    if (Kreuzung[23] == "SP")
                    {
                        Kreuzung[23] = "A7";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "B2")
                {
                    if (Kreuzung[2] == "SP")
                    {
                        Kreuzung[2] = "B2";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "B4")
                {
                    if (Kreuzung[3] == "SP")
                    {
                        Kreuzung[3] = "A4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "B6")
                {
                    if (Kreuzung[4] == "SP")
                    {
                        Kreuzung[4] = "A4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "C3")
                {
                    if (Kreuzung[5] == "SP")
                    {
                        Kreuzung[5] = "C3";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "C4")
                {
                    if (Kreuzung[6] == "SP")
                    {
                        Kreuzung[6] = "C4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "C5")
                {
                    if (Kreuzung[7] == "SP")
                    {
                        Kreuzung[7] = "C5";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "D1")
                {
                    if (Kreuzung[8] == "SP")
                    {
                        Kreuzung[8] = "D1";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "D2")
                {
                    if (Kreuzung[9] == "SP")
                    {
                        Kreuzung[9] = "D2";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "D3")
                {
                    if (Kreuzung[10] == "SP")
                    {
                        Kreuzung[10] = "D3";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "D5")
                {
                    if (Kreuzung[11] == "SP")
                    {
                        Kreuzung[11] = "D5";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "D6")
                {
                    if (Kreuzung[12] == "SP")
                    {
                        Kreuzung[12] = "D6";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "D7")
                {
                    if (Kreuzung[13] == "SP")
                    {
                        Kreuzung[13] = "D7";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "E3")
                {
                    if (Kreuzung[14] == "SP")
                    {
                        Kreuzung[14] = "E3";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "E4")
                {
                    if (Kreuzung[15] == "SP")
                    {
                        Kreuzung[15] = "E4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "E5")
                {
                    if (Kreuzung[16] == "SP")
                    {
                        Kreuzung[16] = "E5";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "F2")
                {
                    if (Kreuzung[17] == "SP")
                    {
                        Kreuzung[17] = "F2";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "F4")
                {
                    if (Kreuzung[18] == "SP")
                    {
                        Kreuzung[18] = "F4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "F6")
                {
                    if (Kreuzung[19] == "SP")
                    {
                        Kreuzung[19] = "F6";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "G1")
                {
                    if (Kreuzung[20] == "SP")
                    {
                        Kreuzung[20] = "G1";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "G4")
                {
                    if (Kreuzung[21] == "SP")
                    {
                        Kreuzung[21] = "G4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "G7")
                {
                    if (Kreuzung[22] == "SP")
                    {
                        Kreuzung[22] = "G7";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }

                Zeichne_Spielfeld(ref Kreuzung);
            }
        }

        #endregion

        #endregion

        #region PHASE 2

        #region Beginne_Phase_2

        static void Beginne_Phase_2
        (
            ref int SpielerSteine, ref int MaxSpielerSteine, ref int ComputerSteine, ref int MaxComputerSteine, ref string[] Kreuzung
        )
        {
            SpielerSteine = 0;
            MaxSpielerSteine = 3;
            ComputerSteine = 0;
            MaxComputerSteine = 3;

            for (int i = 0; i < Kreuzung.Length; i++)
            {
                if (Kreuzung[i] == "SP")
                    SpielerSteine++;
                else if (Kreuzung[i] == "KI")
                    ComputerSteine++;
            }

            Console.Title = "Mühle - PHASE 2: Versetzen";
        }

        #endregion

        #region Spiele_Phase_2

        static void Spiele_Phase_2
        (
            ref int SpielerSteine, ref int MaxSpielerSteine, ref int ComputerSteine, ref int MaxComputerSteine, ref string[] Kreuzung, 
            ref bool Spieler, ref bool Computer, ref bool SteinNehmen
        )
        {
            while (SpielerSteine > MaxSpielerSteine && ComputerSteine > MaxComputerSteine)
            {
                Versetze_Spieler_Stein(ref ComputerSteine, ref Kreuzung, ref Spieler, ref Computer, ref SteinNehmen);

                Versetze_Computer_Stein(ref SpielerSteine, ref Kreuzung, ref Spieler, ref Computer, ref SteinNehmen);
            }
        }

        #endregion

        #region Versetze_Spieler_Stein

        static void Versetze_Spieler_Stein
        (
            ref int ComputerSteine, ref string[] Kreuzung, ref bool Spieler, ref bool Computer, ref bool SteinNehmen
        )
        {
            string SteinStartPosition;
            string SteinZielPosition;

            while (Spieler)
            {
                Console.Write("Wähle dein Stein: ");
                SteinStartPosition = Console.ReadLine().ToUpper();

                Console.Write("Versetze dein Stein: ");
                SteinZielPosition = Console.ReadLine().ToUpper();

                if (SteinStartPosition == "A1" && Kreuzung[0] == "SP")
                {
                    if (SteinZielPosition == "A4" && Kreuzung[1] == "A4")
                    {
                        Kreuzung[0] = "A1";
                        Kreuzung[1] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "D1" && Kreuzung[8] == "D1")
                    {
                        Kreuzung[0] = "A1";
                        Kreuzung[8] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "A4" && Kreuzung[1] == "SP")
                {
                    if (SteinZielPosition == "A1" && Kreuzung[0] == "A1")
                    {
                        Kreuzung[1] = "A4";
                        Kreuzung[0] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "B4" && Kreuzung[3] == "B4")
                    {
                        Kreuzung[1] = "A4";
                        Kreuzung[3] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "A7" && Kreuzung[23] == "A7")
                    {
                        Kreuzung[1] = "A4";
                        Kreuzung[23] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "A7" && Kreuzung[23] == "SP")
                {
                    if (SteinZielPosition == "A4" && Kreuzung[1] == "A4")
                    {
                        Kreuzung[23] = "A7";
                        Kreuzung[1] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "D7" && Kreuzung[13] == "D7")
                    {
                        Kreuzung[23] = "A7";
                        Kreuzung[13] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "B2" && Kreuzung[2] == "SP")
                {
                    if (SteinZielPosition == "B4" && Kreuzung[3] == "B4")
                    {
                        Kreuzung[2] = "B2";
                        Kreuzung[3] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "D2" && Kreuzung[9] == "D2")
                    {
                        Kreuzung[2] = "B2";
                        Kreuzung[9] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "B4" && Kreuzung[3] == "SP")
                {
                    if (SteinZielPosition == "A4" && Kreuzung[1] == "A4")
                    {
                        Kreuzung[3] = "B4";
                        Kreuzung[1] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "B2" && Kreuzung[2] == "B2")
                    {
                        Kreuzung[3] = "B4";
                        Kreuzung[2] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "C4" && Kreuzung[6] == "C4")
                    {
                        Kreuzung[3] = "B4";
                        Kreuzung[6] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "B6" && Kreuzung[4] == "B6")
                    {
                        Kreuzung[3] = "B4";
                        Kreuzung[4] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "B6" && Kreuzung[4] == "SP")
                {
                    if (SteinZielPosition == "B4" && Kreuzung[3] == "B4")
                    {
                        Kreuzung[4] = "B6";
                        Kreuzung[3] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "D6" && Kreuzung[12] == "D6")
                    {
                        Kreuzung[4] = "B6";
                        Kreuzung[12] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "C3" && Kreuzung[5] == "SP")
                {
                    if (SteinZielPosition == "C4" && Kreuzung[6] == "C4")
                    {
                        Kreuzung[5] = "C3";
                        Kreuzung[6] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "D3" && Kreuzung[10] == "D3")
                    {
                        Kreuzung[5] = "C3";
                        Kreuzung[10] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "C4" && Kreuzung[6] == "SP")
                {
                    if (SteinZielPosition == "C3" && Kreuzung[5] == "C3")
                    {
                        Kreuzung[6] = "C4";
                        Kreuzung[5] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "C5" && Kreuzung[7] == "C5")
                    {
                        Kreuzung[6] = "C4";
                        Kreuzung[3] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "B4" && Kreuzung[3] == "B4")
                    {
                        Kreuzung[6] = "C4";
                        Kreuzung[3] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "C5" && Kreuzung[7] == "SP")
                {
                    if (SteinZielPosition == "C4" && Kreuzung[6] == "C4")
                    {
                        Kreuzung[7] = "C5";
                        Kreuzung[6] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "D5" && Kreuzung[11] == "D5")
                    {
                        Kreuzung[7] = "C5";
                        Kreuzung[11] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "D1" && Kreuzung[8] == "SP")
                {
                    if (SteinZielPosition == "A1" && Kreuzung[0] == "A1")
                    {
                        Kreuzung[8] = "D1";
                        Kreuzung[0] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "G1" && Kreuzung[20] == "G1")
                    {
                        Kreuzung[8] = "D1";
                        Kreuzung[20] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "D2" && Kreuzung[9] == "SP")
                {
                    if (SteinZielPosition == "B2" && Kreuzung[2] == "B2")
                    {
                        Kreuzung[9] = "D2";
                        Kreuzung[2] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "F2" && Kreuzung[17] == "F2")
                    {
                        Kreuzung[9] = "D2";
                        Kreuzung[17] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "D3" && Kreuzung[10] == "SP")
                {
                    if (SteinZielPosition == "C3" && Kreuzung[5] == "C3")
                    {
                        Kreuzung[10] = "D3";
                        Kreuzung[5] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "E3" && Kreuzung[14] == "E3")
                    {
                        Kreuzung[10] = "D3";
                        Kreuzung[14] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "D5" && Kreuzung[11] == "SP")
                {
                    if (SteinZielPosition == "C5" && Kreuzung[7] == "C5")
                    {
                        Kreuzung[11] = "D5";
                        Kreuzung[7] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "E5" && Kreuzung[16] == "E5")
                    {
                        Kreuzung[11] = "D5";
                        Kreuzung[16] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "D6" && Kreuzung[12] == "SP")
                {
                    if (SteinZielPosition == "B6" && Kreuzung[4] == "B6")
                    {
                        Kreuzung[12] = "D6";
                        Kreuzung[4] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "F6" && Kreuzung[19] == "F6")
                    {
                        Kreuzung[12] = "D6";
                        Kreuzung[19] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "D7" && Kreuzung[13] == "SP")
                {
                    if (SteinZielPosition == "A7" && Kreuzung[23] == "A7")
                    {
                        Kreuzung[13] = "D7";
                        Kreuzung[23] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "G7" && Kreuzung[22] == "G7")
                    {
                        Kreuzung[13] = "D7";
                        Kreuzung[22] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "E3" && Kreuzung[14] == "SP")
                {
                    if (SteinZielPosition == "D3" && Kreuzung[10] == "D3")
                    {
                        Kreuzung[14] = "E3";
                        Kreuzung[10] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "E4" && Kreuzung[15] == "E4")
                    {
                        Kreuzung[14] = "E3";
                        Kreuzung[15] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "E4" && Kreuzung[15] == "SP")
                {
                    if (SteinZielPosition == "E3" && Kreuzung[14] == "E3")
                    {
                        Kreuzung[15] = "E4";
                        Kreuzung[14] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "E5" && Kreuzung[16] == "E5")
                    {
                        Kreuzung[15] = "E4";
                        Kreuzung[16] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "F4" && Kreuzung[18] == "F4")
                    {
                        Kreuzung[15] = "E4";
                        Kreuzung[18] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "E5" && Kreuzung[16] == "SP")
                {
                    if (SteinZielPosition == "D5" && Kreuzung[11] == "D5")
                    {
                        Kreuzung[16] = "E5";
                        Kreuzung[11] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "E4" && Kreuzung[15] == "E4")
                    {
                        Kreuzung[16] = "E5";
                        Kreuzung[15] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "F2" && Kreuzung[17] == "SP")
                {
                    if (SteinZielPosition == "D2" && Kreuzung[9] == "D2")
                    {
                        Kreuzung[17] = "F2";
                        Kreuzung[9] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "F4" && Kreuzung[18] == "F4")
                    {
                        Kreuzung[17] = "F2";
                        Kreuzung[18] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "F4" && Kreuzung[18] == "SP")
                {
                    if (SteinZielPosition == "F2" && Kreuzung[17] == "F2")
                    {
                        Kreuzung[18] = "F4";
                        Kreuzung[17] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "F6" && Kreuzung[19] == "F6")
                    {
                        Kreuzung[18] = "F4";
                        Kreuzung[19] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "F6" && Kreuzung[19] == "SP")
                {
                    if (SteinZielPosition == "D6" && Kreuzung[12] == "D6")
                    {
                        Kreuzung[19] = "F6";
                        Kreuzung[12] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "F4" && Kreuzung[18] == "F4")
                    {
                        Kreuzung[19] = "F6";
                        Kreuzung[18] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "G1" && Kreuzung[20] == "SP")
                {
                    if (SteinZielPosition == "D1" && Kreuzung[8] == "D1")
                    {
                        Kreuzung[20] = "G1";
                        Kreuzung[8] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "G4" && Kreuzung[21] == "G4")
                    {
                        Kreuzung[20] = "G1";
                        Kreuzung[21] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "G4" && Kreuzung[21] == "SP")
                {
                    if (SteinZielPosition == "F4" && Kreuzung[18] == "F4")
                    {
                        Kreuzung[21] = "G4";
                        Kreuzung[18] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "G1" && Kreuzung[20] == "G1")
                    {
                        Kreuzung[21] = "G4";
                        Kreuzung[20] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "G7" && Kreuzung[22] == "G7")
                    {
                        Kreuzung[21] = "G4";
                        Kreuzung[22] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }
                else if (SteinStartPosition == "G7" && Kreuzung[22] == "SP")
                {
                    if (SteinZielPosition == "D7" && Kreuzung[13] == "D7")
                    {
                        Kreuzung[22] = "G7";
                        Kreuzung[13] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                    else if (SteinZielPosition == "G4" && Kreuzung[21] == "G4")
                    {
                        Kreuzung[22] = "G7";
                        Kreuzung[21] = "SP";

                        Console.Clear();

                        Spieler = false;
                        Computer = true;
                    }
                }

                Zeichne_Spielfeld(ref Kreuzung);

                Nimm_KI_Stein_Phase_2(ref Kreuzung, ref SteinNehmen, ref ComputerSteine);

            }
        }

        #endregion
        #region Nimm_KI_Stein_Phase_2

        static void Nimm_KI_Stein_Phase_2(ref string[] Kreuzung, ref bool SteinNehmen, ref int ComputerSteine)
        {
            if (Kreuzung[23] == "SP" && Kreuzung[13] == "SP" && Kreuzung[22] == "SP" ||
                Kreuzung[23] == "SP" && Kreuzung[1] == "SP" && Kreuzung[0] == "SP" ||
                Kreuzung[20] == "SP" && Kreuzung[21] == "SP" && Kreuzung[22] == "SP" ||
                Kreuzung[20] == "SP" && Kreuzung[8] == "SP" && Kreuzung[0] == "SP" ||
                Kreuzung[1] == "SP" && Kreuzung[3] == "SP" && Kreuzung[6] == "SP" ||
                Kreuzung[15] == "SP" && Kreuzung[18] == "SP" && Kreuzung[21] == "SP" ||
                Kreuzung[4] == "SP" && Kreuzung[3] == "SP" && Kreuzung[2] == "SP" ||
                Kreuzung[4] == "SP" && Kreuzung[12] == "SP" && Kreuzung[19] == "SP" ||
                Kreuzung[17] == "SP" && Kreuzung[18] == "SP" && Kreuzung[19] == "SP" ||
                Kreuzung[17] == "SP" && Kreuzung[9] == "SP" && Kreuzung[2] == "SP" ||
                Kreuzung[7] == "SP" && Kreuzung[6] == "SP" && Kreuzung[5] == "SP" ||
                Kreuzung[7] == "SP" && Kreuzung[11] == "SP" && Kreuzung[16] == "SP" ||
                Kreuzung[14] == "SP" && Kreuzung[15] == "SP" && Kreuzung[16] == "SP" ||
                Kreuzung[14] == "SP" && Kreuzung[10] == "SP" && Kreuzung[5] == "SP")
            {
                SteinNehmen = true;

                ComputerSteine--;
            }
            string Eingabe = "";

            while (SteinNehmen)
            {
                Console.Write("Such' dir ein KI-Stein aus: ");
                Eingabe = Console.ReadLine().ToUpper();

                if (Eingabe == "A1")
                {
                    if (Kreuzung[0] == "KI")
                    {
                        Kreuzung[0] = "A1";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "A4")
                {
                    if (Kreuzung[1] == "KI")
                    {
                        Kreuzung[1] = "A4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "A7")
                {
                    if (Kreuzung[23] == "KI")
                    {
                        Kreuzung[23] = "A7";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "B2")
                {
                    if (Kreuzung[2] == "KI")
                    {
                        Kreuzung[2] = "B2";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "B4")
                {
                    if (Kreuzung[3] == "KI")
                    {
                        Kreuzung[3] = "A4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "B6")
                {
                    if (Kreuzung[4] == "KI")
                    {
                        Kreuzung[4] = "A4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "C3")
                {
                    if (Kreuzung[5] == "KI")
                    {
                        Kreuzung[5] = "C3";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "C4")
                {
                    if (Kreuzung[6] == "KI")
                    {
                        Kreuzung[6] = "C4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "C5")
                {
                    if (Kreuzung[7] == "KI")
                    {
                        Kreuzung[7] = "C5";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "D1")
                {
                    if (Kreuzung[8] == "KI")
                    {
                        Kreuzung[8] = "D1";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "D2")
                {
                    if (Kreuzung[9] == "KI")
                    {
                        Kreuzung[9] = "D2";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "D3")
                {
                    if (Kreuzung[10] == "KI")
                    {
                        Kreuzung[10] = "D3";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "D5")
                {
                    if (Kreuzung[11] == "KI")
                    {
                        Kreuzung[11] = "D5";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "D6")
                {
                    if (Kreuzung[12] == "KI")
                    {
                        Kreuzung[12] = "D6";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "D7")
                {
                    if (Kreuzung[13] == "KI")
                    {
                        Kreuzung[13] = "D7";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "E3")
                {
                    if (Kreuzung[14] == "KI")
                    {
                        Kreuzung[14] = "E3";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "E4")
                {
                    if (Kreuzung[15] == "KI")
                    {
                        Kreuzung[15] = "E4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "E5")
                {
                    if (Kreuzung[16] == "KI")
                    {
                        Kreuzung[16] = "E5";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "F2")
                {
                    if (Kreuzung[17] == "KI")
                    {
                        Kreuzung[17] = "F2";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "F4")
                {
                    if (Kreuzung[18] == "KI")
                    {
                        Kreuzung[18] = "F4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "F6")
                {
                    if (Kreuzung[19] == "KI")
                    {
                        Kreuzung[19] = "F6";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "G1")
                {
                    if (Kreuzung[20] == "KI")
                    {
                        Kreuzung[20] = "G1";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "G4")
                {
                    if (Kreuzung[21] == "KI")
                    {
                        Kreuzung[21] = "G4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (Eingabe == "G7")
                {
                    if (Kreuzung[22] == "KI")
                    {
                        Kreuzung[22] = "G7";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }

                Zeichne_Spielfeld(ref Kreuzung);
            }
        }

        #endregion

        #region Versetze_Computer_Stein

        static void Versetze_Computer_Stein
        (
            ref int SpielerSteine, ref string[] Kreuzung, ref bool Spieler, ref bool Computer, ref bool SteinNehmen
        )
        {
            Random Zufall = new Random();
            string SteinStartPosition;
            string SteinZielPosition;
            string[] ComputerVersetzMoeglichkeiten = { };

            while (Computer)
            {
                SteinStartPosition = Kreuzung[Zufall.Next(0, Kreuzung.Length)];

                if (SteinStartPosition == "A1" && Kreuzung[0] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "A4";
                    ComputerVersetzMoeglichkeiten[2] = "D1";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "A4" && Kreuzung[1] == "A4")
                    {
                        Kreuzung[0] = "A1";
                        Kreuzung[1] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "D1" && Kreuzung[8] == "D1")
                    {
                        Kreuzung[0] = "A1";
                        Kreuzung[8] = "SP";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "A4" && Kreuzung[1] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "A1";
                    ComputerVersetzMoeglichkeiten[2] = "B4";
                    ComputerVersetzMoeglichkeiten[3] = "A7";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "A1" && Kreuzung[0] == "A1")
                    {
                        Kreuzung[1] = "A4";
                        Kreuzung[0] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "B4" && Kreuzung[3] == "B4")
                    {
                        Kreuzung[1] = "A4";
                        Kreuzung[3] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "A7" && Kreuzung[23] == "A7")
                    {
                        Kreuzung[1] = "A4";
                        Kreuzung[23] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "A7" && Kreuzung[23] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "A4";
                    ComputerVersetzMoeglichkeiten[2] = "D7";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "A4" && Kreuzung[1] == "A4")
                    {
                        Kreuzung[23] = "A7";
                        Kreuzung[1] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "D7" && Kreuzung[13] == "D7")
                    {
                        Kreuzung[23] = "A7";
                        Kreuzung[13] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "B2" && Kreuzung[2] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "B4";
                    ComputerVersetzMoeglichkeiten[2] = "D2";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "B4" && Kreuzung[3] == "B4")
                    {
                        Kreuzung[2] = "B2";
                        Kreuzung[3] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "D2" && Kreuzung[9] == "D2")
                    {
                        Kreuzung[2] = "B2";
                        Kreuzung[9] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "B4" && Kreuzung[3] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "A4";
                    ComputerVersetzMoeglichkeiten[2] = "B2";
                    ComputerVersetzMoeglichkeiten[3] = "C4";
                    ComputerVersetzMoeglichkeiten[4] = "B6";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "A4" && Kreuzung[1] == "A4")
                    {
                        Kreuzung[3] = "B4";
                        Kreuzung[1] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "B2" && Kreuzung[2] == "B2")
                    {
                        Kreuzung[3] = "B4";
                        Kreuzung[2] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "C4" && Kreuzung[6] == "C4")
                    {
                        Kreuzung[3] = "B4";
                        Kreuzung[6] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "B6" && Kreuzung[4] == "B6")
                    {
                        Kreuzung[3] = "B4";
                        Kreuzung[4] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "B6" && Kreuzung[4] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "B4";
                    ComputerVersetzMoeglichkeiten[2] = "D6";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "B4" && Kreuzung[3] == "B4")
                    {
                        Kreuzung[4] = "B6";
                        Kreuzung[3] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "D6" && Kreuzung[12] == "D6")
                    {
                        Kreuzung[4] = "B6";
                        Kreuzung[12] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "C3" && Kreuzung[5] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "C4";
                    ComputerVersetzMoeglichkeiten[2] = "D3";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "C4" && Kreuzung[6] == "C4")
                    {
                        Kreuzung[5] = "C3";
                        Kreuzung[6] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "D3" && Kreuzung[10] == "D3")
                    {
                        Kreuzung[5] = "C3";
                        Kreuzung[10] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "C4" && Kreuzung[6] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "C3";
                    ComputerVersetzMoeglichkeiten[2] = "C5";
                    ComputerVersetzMoeglichkeiten[3] = "B4";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "C3" && Kreuzung[5] == "C3")
                    {
                        Kreuzung[6] = "C4";
                        Kreuzung[5] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "C5" && Kreuzung[7] == "C5")
                    {
                        Kreuzung[6] = "C4";
                        Kreuzung[3] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "B4" && Kreuzung[3] == "B4")
                    {
                        Kreuzung[6] = "C4";
                        Kreuzung[3] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "C5" && Kreuzung[7] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "C4";
                    ComputerVersetzMoeglichkeiten[2] = "D5";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "C4" && Kreuzung[6] == "C4")
                    {
                        Kreuzung[7] = "C5";
                        Kreuzung[6] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "D5" && Kreuzung[11] == "D5")
                    {
                        Kreuzung[7] = "C5";
                        Kreuzung[11] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "D1" && Kreuzung[8] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "A1";
                    ComputerVersetzMoeglichkeiten[2] = "G1";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "A1" && Kreuzung[0] == "A1")
                    {
                        Kreuzung[8] = "D1";
                        Kreuzung[0] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "G1" && Kreuzung[20] == "G1")
                    {
                        Kreuzung[8] = "D1";
                        Kreuzung[20] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "D2" && Kreuzung[9] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "B2";
                    ComputerVersetzMoeglichkeiten[2] = "F2";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "B2" && Kreuzung[2] == "B2")
                    {
                        Kreuzung[9] = "D2";
                        Kreuzung[2] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "F2" && Kreuzung[17] == "F2")
                    {
                        Kreuzung[9] = "D2";
                        Kreuzung[17] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "D3" && Kreuzung[10] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "C3";
                    ComputerVersetzMoeglichkeiten[2] = "E3";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "C3" && Kreuzung[5] == "C3")
                    {
                        Kreuzung[10] = "D3";
                        Kreuzung[5] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "E3" && Kreuzung[14] == "E3")
                    {
                        Kreuzung[10] = "D3";
                        Kreuzung[14] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "D5" && Kreuzung[11] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "C5";
                    ComputerVersetzMoeglichkeiten[2] = "E5";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "C5" && Kreuzung[7] == "C5")
                    {
                        Kreuzung[11] = "D5";
                        Kreuzung[7] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "E5" && Kreuzung[16] == "E5")
                    {
                        Kreuzung[11] = "D5";
                        Kreuzung[16] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "D6" && Kreuzung[12] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "B6";
                    ComputerVersetzMoeglichkeiten[2] = "F6";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "B6" && Kreuzung[4] == "B6")
                    {
                        Kreuzung[12] = "D6";
                        Kreuzung[4] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "F6" && Kreuzung[19] == "F6")
                    {
                        Kreuzung[12] = "D6";
                        Kreuzung[19] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "D7" && Kreuzung[13] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "A7";
                    ComputerVersetzMoeglichkeiten[2] = "G7";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "A7" && Kreuzung[23] == "A7")
                    {
                        Kreuzung[13] = "D7";
                        Kreuzung[23] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "G7" && Kreuzung[22] == "G7")
                    {
                        Kreuzung[13] = "D7";
                        Kreuzung[22] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "E3" && Kreuzung[14] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "D3";
                    ComputerVersetzMoeglichkeiten[2] = "E4";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "D3" && Kreuzung[10] == "D3")
                    {
                        Kreuzung[14] = "E3";
                        Kreuzung[10] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "E4" && Kreuzung[15] == "E4")
                    {
                        Kreuzung[14] = "E3";
                        Kreuzung[15] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "E4" && Kreuzung[15] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "E3";
                    ComputerVersetzMoeglichkeiten[2] = "E5";
                    ComputerVersetzMoeglichkeiten[3] = "F4";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "E3" && Kreuzung[14] == "E3")
                    {
                        Kreuzung[15] = "E4";
                        Kreuzung[14] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "E5" && Kreuzung[16] == "E5")
                    {
                        Kreuzung[15] = "E4";
                        Kreuzung[16] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "F4" && Kreuzung[18] == "F4")
                    {
                        Kreuzung[15] = "E4";
                        Kreuzung[18] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "E5" && Kreuzung[16] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "D5";
                    ComputerVersetzMoeglichkeiten[2] = "E4";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "D5" && Kreuzung[11] == "D5")
                    {
                        Kreuzung[16] = "E5";
                        Kreuzung[11] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "E4" && Kreuzung[15] == "E4")
                    {
                        Kreuzung[16] = "E5";
                        Kreuzung[15] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "F2" && Kreuzung[17] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "D2";
                    ComputerVersetzMoeglichkeiten[2] = "F4";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "D2" && Kreuzung[9] == "D2")
                    {
                        Kreuzung[17] = "F2";
                        Kreuzung[9] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "F4" && Kreuzung[18] == "F4")
                    {
                        Kreuzung[17] = "F2";
                        Kreuzung[18] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "F4" && Kreuzung[18] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "F2";
                    ComputerVersetzMoeglichkeiten[2] = "F6";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "F2" && Kreuzung[17] == "F2")
                    {
                        Kreuzung[18] = "F4";
                        Kreuzung[17] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "F6" && Kreuzung[19] == "F6")
                    {
                        Kreuzung[18] = "F4";
                        Kreuzung[19] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "F6" && Kreuzung[19] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "D6";
                    ComputerVersetzMoeglichkeiten[2] = "F4";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "D6" && Kreuzung[12] == "D6")
                    {
                        Kreuzung[19] = "F6";
                        Kreuzung[12] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "F4" && Kreuzung[18] == "F4")
                    {
                        Kreuzung[19] = "F6";
                        Kreuzung[18] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "G1" && Kreuzung[20] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "D1";
                    ComputerVersetzMoeglichkeiten[2] = "G4";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "D1" && Kreuzung[8] == "D1")
                    {
                        Kreuzung[20] = "G1";
                        Kreuzung[8] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "G4" && Kreuzung[21] == "G4")
                    {
                        Kreuzung[20] = "G1";
                        Kreuzung[21] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "G4" && Kreuzung[21] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "F4";
                    ComputerVersetzMoeglichkeiten[2] = "G1";
                    ComputerVersetzMoeglichkeiten[3] = "G7";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "F4" && Kreuzung[18] == "F4")
                    {
                        Kreuzung[21] = "G4";
                        Kreuzung[18] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "G1" && Kreuzung[20] == "G1")
                    {
                        Kreuzung[21] = "G4";
                        Kreuzung[20] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "G7" && Kreuzung[22] == "G7")
                    {
                        Kreuzung[21] = "G4";
                        Kreuzung[22] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }
                else if (SteinStartPosition == "G7" && Kreuzung[22] == "KI")
                {
                    ComputerVersetzMoeglichkeiten[1] = "D7";
                    ComputerVersetzMoeglichkeiten[2] = "G4";

                    SteinZielPosition = ComputerVersetzMoeglichkeiten[Zufall.Next(0, ComputerVersetzMoeglichkeiten.Length)];

                    if (SteinZielPosition == "D7" && Kreuzung[13] == "D7")
                    {
                        Kreuzung[22] = "G7";
                        Kreuzung[13] = "KI";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                    else if (SteinZielPosition == "G4" && Kreuzung[21] == "G4")
                    {
                        Kreuzung[22] = "G7";
                        Kreuzung[21] = "SP";

                        Console.Clear();

                        Spieler = true;
                        Computer = false;
                    }
                }

                Zeichne_Spielfeld(ref Kreuzung);

                Nimm_Spieler_Stein_Phase_2(ref Kreuzung, ref SteinNehmen, ref SpielerSteine);
            }
        }

        #endregion
        #region Nimm_Spieler_Stein_Phase_2

        static void Nimm_Spieler_Stein_Phase_2(ref string[] Kreuzung, ref bool SteinNehmen, ref int SpielerSteine)
        {
            Random Zufall = new Random();
            string KI;

            if (Kreuzung[23] == "KI" && Kreuzung[13] == "KI" && Kreuzung[22] == "KI" ||
                Kreuzung[23] == "KI" && Kreuzung[1] == "KI" && Kreuzung[0] == "KI" ||
                Kreuzung[20] == "KI" && Kreuzung[21] == "KI" && Kreuzung[22] == "KI" ||
                Kreuzung[20] == "KI" && Kreuzung[8] == "KI" && Kreuzung[0] == "KI" ||
                Kreuzung[1] == "KI" && Kreuzung[3] == "KI" && Kreuzung[6] == "KI" ||
                Kreuzung[15] == "KI" && Kreuzung[18] == "KI" && Kreuzung[21] == "KI" ||
                Kreuzung[4] == "KI" && Kreuzung[3] == "KI" && Kreuzung[2] == "KI" ||
                Kreuzung[4] == "KI" && Kreuzung[12] == "KI" && Kreuzung[19] == "KI" ||
                Kreuzung[17] == "KI" && Kreuzung[18] == "KI" && Kreuzung[19] == "KI" ||
                Kreuzung[17] == "KI" && Kreuzung[9] == "KI" && Kreuzung[2] == "KI" ||
                Kreuzung[7] == "KI" && Kreuzung[6] == "KI" && Kreuzung[5] == "KI" ||
                Kreuzung[7] == "KI" && Kreuzung[11] == "KI" && Kreuzung[16] == "KI" ||
                Kreuzung[14] == "KI" && Kreuzung[15] == "KI" && Kreuzung[16] == "KI" ||
                Kreuzung[14] == "KI" && Kreuzung[10] == "KI" && Kreuzung[5] == "KI")
            {
                SteinNehmen = true;

                SpielerSteine--;
            }

            while (SteinNehmen)
            {
                KI = Kreuzung[Zufall.Next(0, Kreuzung.Length)];

                if (KI == "A1")
                {
                    if (Kreuzung[0] == "SP")
                    {
                        Kreuzung[0] = "A1";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "A4")
                {
                    if (Kreuzung[1] == "SP")
                    {
                        Kreuzung[1] = "A4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "A7")
                {
                    if (Kreuzung[23] == "SP")
                    {
                        Kreuzung[23] = "A7";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "B2")
                {
                    if (Kreuzung[2] == "SP")
                    {
                        Kreuzung[2] = "B2";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "B4")
                {
                    if (Kreuzung[3] == "SP")
                    {
                        Kreuzung[3] = "A4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "B6")
                {
                    if (Kreuzung[4] == "SP")
                    {
                        Kreuzung[4] = "A4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "C3")
                {
                    if (Kreuzung[5] == "SP")
                    {
                        Kreuzung[5] = "C3";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "C4")
                {
                    if (Kreuzung[6] == "SP")
                    {
                        Kreuzung[6] = "C4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "C5")
                {
                    if (Kreuzung[7] == "SP")
                    {
                        Kreuzung[7] = "C5";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "D1")
                {
                    if (Kreuzung[8] == "SP")
                    {
                        Kreuzung[8] = "D1";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "D2")
                {
                    if (Kreuzung[9] == "SP")
                    {
                        Kreuzung[9] = "D2";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "D3")
                {
                    if (Kreuzung[10] == "SP")
                    {
                        Kreuzung[10] = "D3";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "D5")
                {
                    if (Kreuzung[11] == "SP")
                    {
                        Kreuzung[11] = "D5";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "D6")
                {
                    if (Kreuzung[12] == "SP")
                    {
                        Kreuzung[12] = "D6";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "D7")
                {
                    if (Kreuzung[13] == "SP")
                    {
                        Kreuzung[13] = "D7";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "E3")
                {
                    if (Kreuzung[14] == "SP")
                    {
                        Kreuzung[14] = "E3";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "E4")
                {
                    if (Kreuzung[15] == "SP")
                    {
                        Kreuzung[15] = "E4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "E5")
                {
                    if (Kreuzung[16] == "SP")
                    {
                        Kreuzung[16] = "E5";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "F2")
                {
                    if (Kreuzung[17] == "SP")
                    {
                        Kreuzung[17] = "F2";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "F4")
                {
                    if (Kreuzung[18] == "SP")
                    {
                        Kreuzung[18] = "F4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "F6")
                {
                    if (Kreuzung[19] == "SP")
                    {
                        Kreuzung[19] = "F6";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "G1")
                {
                    if (Kreuzung[20] == "SP")
                    {
                        Kreuzung[20] = "G1";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "G4")
                {
                    if (Kreuzung[21] == "SP")
                    {
                        Kreuzung[21] = "G4";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }
                else if (KI == "G7")
                {
                    if (Kreuzung[22] == "SP")
                    {
                        Kreuzung[22] = "G7";

                        Console.Clear();

                        SteinNehmen = false;
                    }
                }

                Zeichne_Spielfeld(ref Kreuzung);
            }
        }

        #endregion

        #endregion

        #region PHASE 3

        #region Beginne_Phase_3

        static void Beginne_Phase_3(ref int MaxSpielerSteine, ref int MaxComputerSteine)
        {
            MaxSpielerSteine = 0;
            MaxComputerSteine = 0;

            Console.Title = "Mühle - PHASE 3: Endspiel";
        }

        #endregion

        #endregion
    }
}