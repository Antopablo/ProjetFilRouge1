﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetFilRouge1
{
    enum Stats
    {
        attaque,
        defense,
        vitesse,
        pv
    }

    class Program
    {
        static void Main(string[] args)
        {


            Random randomCalc = new Random();

            Console.WriteLine(" ########### Création de personnage(s) ########### ");
            List <Personnage> ListeTeam = CreationTeam();
            Console.WriteLine("########### Commençons l'aventure .. ###########");
            List<Mob> ListeDennemi = CreationEnnemi();
            List<Boss> ListeBoss = CreationBoss();
            int nbEnnemi = ListeDennemi.Count;
            int nbTeam = ListeTeam.Count;
            int nbBoss = ListeBoss.Count;
            Random gen = new Random();
            bool continu = true;
            int randomEnnemi = randomCalc.Next(0, nbEnnemi);
            int randomTeam = randomCalc.Next(0, nbTeam);
            string UserChoice = "";


            do
            {
                Console.WriteLine("\r\n ########### Appuyer sur X pour commencer le combat ########### ");
                UserChoice = Console.ReadLine().ToUpper();
            } while (UserChoice != "X");
            UserChoice = " ";
            do
            {
                // team attaque
                while (continu == true && nbEnnemi > 0 && nbTeam > 0)
                {
                    randomEnnemi = randomCalc.Next(0, nbEnnemi);

                    randomTeam = randomCalc.Next(0, nbTeam);
                    Console.WriteLine("\r\n");
                    ListeTeam[randomTeam].Attaquer(ListeDennemi[randomEnnemi]);
                    Console.WriteLine("\r\n");
                    if (ListeDennemi[randomEnnemi].HP == 0)
                    {
                        ListeDennemi.Remove(ListeDennemi[randomEnnemi]);
                        nbEnnemi--;
                    }
                    continu = gen.Next(100) < 60 ? true : false;   //60% de chance de refaire une attaque
                    Console.WriteLine("il reste " + nbEnnemi + " ennemi");

                    if (nbEnnemi == 0)
                    {
                        Console.WriteLine(" ########### Tu as battu tous les ennemis, mais un bruit étrange se fait entendre .... ########### ");
                    }
                }

                // ennemi attaque
                while (continu == false && nbTeam > 0 && nbEnnemi > 0)
                {
                    randomEnnemi = randomCalc.Next(0, nbEnnemi);
                    randomTeam = randomCalc.Next(0, nbTeam);
                    Console.WriteLine("\r\n");
                    ListeDennemi[randomEnnemi].Attaquer(ListeTeam[randomTeam]);
                    Console.WriteLine("\r\n");
                    if (ListeTeam[randomTeam].HP == 0)
                    {
                        ListeTeam.Remove(ListeTeam[randomTeam]);
                        nbTeam--;
                    }
                    continu = gen.Next(100) < 30 ? true : false;   //70% de chance de refaire une attaque
                    Console.WriteLine("il reste " + nbTeam + " personnage dans la team");

                    if (nbTeam == 0)
                    {
                        Console.WriteLine("Nul, tu as perdu");
                        Console.WriteLine("\r\n  FIN DU JEU \r\n ");
                        Environment.Exit(0);                    // Fin du jeu
                    }
                }
            } while (nbEnnemi > 0 && nbTeam > 0);

            Console.WriteLine(" ########### Vous avancez, et vous trouvez une grande quantité de potion de boost de vie ########### ");
            Console.WriteLine(" ########### Voulez-vous booster l'équipe ? ########### ");
            UserChoice = Console.ReadLine().ToUpper();
            if (UserChoice == "OUI")
            {
                foreach (Personnage personnage in ListeTeam)
                {
                    personnage.BoostHpPotion();
                }
            }
            UserChoice = " ";
            do
            {
                Console.WriteLine("Appuyer sur X pour continuer");
                UserChoice = Console.ReadLine().ToUpper();
            } while (UserChoice != "X");
            UserChoice = " ";
            continu = false;
            Console.WriteLine("\r\n  ########### Le bruit se rapproche !! Un boss apparait ! ########### \r\n ");
            CreationBoss();
            Console.WriteLine(" ########### Il s'agit de " + ListeBoss[0].Nom + " ########### ");
            Console.WriteLine(" ########### Voici ses caractéristiques " + ListeBoss[0]);
            do
            {
                Console.WriteLine("Appuyer sur X pour continuer");
                UserChoice = Console.ReadLine().ToUpper();
            } while (UserChoice != "X");
            UserChoice = " ";
            do
            {
                Console.WriteLine("\r\n ########### Le boss nous attaque !! ########### \r\n ");
                Console.WriteLine("\r\n Appuyer sur X pour continuer \r\n");
                UserChoice = Console.ReadLine().ToUpper();
            } while (UserChoice != "X");
            UserChoice = " ";
            do
            {
                //boss attaque
                while (nbBoss > 0 && nbTeam > 0 && continu == false)
                {

                    randomTeam = randomCalc.Next(0, nbTeam);
                    ListeBoss[0].Attaquer(ListeTeam[randomTeam]);
                    if (ListeTeam[randomTeam].HP == 0)
                    {
                        ListeTeam.Remove(ListeTeam[randomTeam]);
                        nbTeam--;
                    }

                    if (ListeBoss[0].HP == 0)
                    {
                        ListeBoss.Remove(ListeBoss[0]);
                        nbBoss--;
                    }
                    continu = gen.Next(100) < 50 ? true : false;  //50% de chance que le boss refasse une attaque
                }
                // attaque team
                while (nbBoss > 0 && nbTeam > 0 && continu == true)
                {
                    randomTeam = randomCalc.Next(0, nbTeam);
                    ListeTeam[randomTeam].Attaquer(ListeBoss[0]);
                    if (ListeTeam[randomTeam].HP == 0)
                    {
                        ListeTeam.Remove(ListeTeam[randomTeam]);
                        nbTeam--;
                    }

                    if (ListeBoss[0].HP == 0)
                    {
                        ListeBoss.Remove(ListeBoss[0]);
                        nbBoss--;
                    }

                    continu = gen.Next(100) < 60 ? true : false; //60% de chance que la team refasse une attaque
                }

                if (nbTeam == 0)
                {
                    Console.WriteLine("\r\n \r\n  ########### Tous les membres de l'équipe sont mort .... Fin du jeu ###########\r\n \r\n  ");
                    Environment.Exit(0);
                }

                if (nbBoss == 0)
                {
                    Console.WriteLine(" \r\n \r\n ########### Le boss est mort !! Le coup final a été porté par " + ListeTeam[randomTeam].Nom + " ########### \r\n \r\n ");

                }
            } while (nbBoss > 0 && nbTeam > 0);

            Console.WriteLine("coucou");
        }

        static List<Mob> CreationEnnemi()
        {
            Random rnd = new Random();
            List<Mob> ListMob = new List<Mob>();
            int crea = rnd.Next(1, 5);              // nombre d'ennemi créé
            for (int i = 0; i < crea; i++)
            {
                ListMob.Add(new Mob("Aibat" + (i + 1)));
            }
            foreach (var x in ListMob)
            {
                Console.WriteLine("Un Aibat sauvage apparait !");
                Console.WriteLine(x);
            }
            Console.WriteLine("Il y a " + ListMob.Count + " ennemi");

            return ListMob;
        }

        static List<Boss> CreationBoss()
        {
            List<Boss> ListeBoss = new List<Boss>();
            ListeBoss.Add(new Boss("Zigator le vampire"));
            return ListeBoss;
        }



        static List<Personnage> CreationTeam()
        {
            Item epee = new Item(10, Stats.attaque, "épée de fer");
            Item slip = new Item(5, Stats.defense, "slip en coton");
            Item baton = new Item(15, Stats.attaque, "bâton de feu");
            Item arc = new Item(10, Stats.attaque, "arc de bois");
            Item chausson = new Item(5, Stats.defense, "chausson licorne");



            Random rnd = new Random();
            List<Personnage> ListTeam = new List<Personnage>();
            Console.WriteLine("Combien de personnage voulez-vous dans la team ?");
            int nbCrea = Int16.Parse(Console.ReadLine());
            Console.WriteLine("Nous allons creer " + nbCrea + " personnages");

            for (int i = 0; i < nbCrea; i++)
            {
                string choixEquip = "";
                string choix = " ";
                while ((choix != "GUERRIER") && (choix != "MAGE") && (choix != "ARCHER"))
                {
                    Console.WriteLine("Que voulez-vous créer ? Guerrier / Mage / Archer ");
                    choix = Console.ReadLine().ToUpper();

                } 

                switch (choix)
                {
                    case "GUERRIER":
                        string nomG = "";
                        do
                        {
                            Console.WriteLine("Bienvenue au nouveau guerrier !! Comment s'appelle t-il ?");
                            nomG = Console.ReadLine();
                        } while (nomG == "");
                        Guerrier guerrier = new Guerrier(nomG);
                        ListTeam.Add(guerrier);
                        Console.WriteLine("Bienvenue " + nomG);
                        do
                        {
                            Console.WriteLine("voulez-vous lui donner une pièce d'armure? oui/non");
                            choix = Console.ReadLine().ToUpper();
                        } while (choix != "OUI" && choix != "NON");

                        if (choix == "OUI")
                        {
                                while ((choixEquip != "CHAUSSON") && (choixEquip != "SLIP") && (choixEquip != "RIEN"))
                                    {
                                    Console.WriteLine("que voulez-vous lui donner ? chausson / slip / rien");
                                    choixEquip = Console.ReadLine().ToUpper();
                                    }
                                    
                                switch (choixEquip)
                                {
                                    case "CHAUSSON":
                                        guerrier.AjouterEquipement(chausson);
                                        Console.WriteLine("Chausson ajouté sur " +nomG);
                                        choixEquip = " ";
                                        break;
                                    case "SLIP":
                                        guerrier.AjouterEquipement(slip);
                                        Console.WriteLine("Slip ajouté sur " +nomG);
                                        choixEquip = " ";
                                        break;
                                    case "RIEN":
                                        Console.WriteLine("Ah VRAIMENT");
                                    choixEquip = " ";
                                    break;
                                }
                        }
                        choix = " ";
                        do
                        {
                            Console.WriteLine("Voulez-vous donner une arme ? oui/non");
                            choix = Console.ReadLine().ToUpper();
                        } while (choix != "OUI" && choix != "NON");
                        if (choix == "OUI")
                        {
                            while (choixEquip !="EPEE" && choixEquip != "BATON" && choixEquip != "ARC" && choixEquip !="RIEN")
                            {
                                Console.WriteLine("que voulez-vous lui donner ? epee / baton / arc / rien");
                                choixEquip = Console.ReadLine().ToUpper();
                            }

                            switch (choixEquip)
                            {
                                case "EPEE":
                                    guerrier.AjouterEquipement(epee);
                                    Console.WriteLine("épée ajouté");
                                    choixEquip = " ";
                                    break;
                                case "BATON":
                                    guerrier.AjouterEquipement(baton);
                                    Console.WriteLine("bâton ajouté");
                                    choixEquip = " ";
                                    break;
                                case "ARC":
                                    guerrier.AjouterEquipement(arc);
                                    Console.WriteLine("arc ajouté");
                                    choixEquip = " ";
                                    break;
                                case "RIEN":
                                    Console.WriteLine("Ah VRAIMENT");
                                    choixEquip = " ";
                                    break;
                            }
                        }
                        Console.WriteLine(nomG + " a bien rejoint notre équipe \r\n");
                        Console.WriteLine(guerrier);
                        break;
                    case "MAGE":
                        string nomM = "";
                        do
                        {
                            Console.WriteLine("Un Mage vient d'apparaitre ! Quel est son nom ?");
                            nomM = Console.ReadLine();
                        } while (nomM == "");

                        Mage mage = new Mage(nomM);
                        ListTeam.Add(mage);
                        Console.WriteLine("Bienvenue " + nomM);
                        do
                        {
                            Console.WriteLine("voulez-vous lui donner une pièce d'armure? oui/non");
                            choix = Console.ReadLine().ToUpper();
                        } while (choix != "OUI" && choix != "NON");
                        if (choix == "OUI")
                        {
                            while ((choixEquip != "CHAUSSON") && (choixEquip != "SLIP") && (choixEquip != "RIEN"))
                            {
                                Console.WriteLine("que voulez-vous lui donner ? chausson / slip / rien");
                                choixEquip = Console.ReadLine().ToUpper();
                            }

                            switch (choixEquip)
                            {
                                case "CHAUSSON":
                                    mage.AjouterEquipement(chausson);
                                    Console.WriteLine("Chausson ajouté sur " + nomM);
                                    choixEquip = " ";
                                    break;
                                case "SLIP":
                                    mage.AjouterEquipement(slip);
                                    Console.WriteLine("Slip ajouté sur " + nomM);
                                    choixEquip = " ";
                                    break;
                                case "RIEN":
                                    Console.WriteLine("Ah VRAIMENT");
                                    choixEquip = " ";
                                    break;
                            }
                        }
                        choix = " ";
                        do
                        {
                            Console.WriteLine("Voulez-vous donner une arme ? oui/non");
                            choix = Console.ReadLine().ToUpper();
                        } while (choix != "OUI" && choix != "NON");

                        if (choix == "OUI")
                        {
                            while (choixEquip != "EPEE" && choixEquip != "BATON" && choixEquip != "ARC" && choixEquip != "RIEN")
                            {
                                Console.WriteLine("que voulez-vous lui donner ? epee / baton / arc / rien");
                                choixEquip = Console.ReadLine().ToUpper();
                            }

                            switch (choixEquip)
                            {
                                case "EPEE":
                                    mage.AjouterEquipement(epee);
                                    Console.WriteLine("épée ajouté");
                                    choixEquip = " ";
                                    break;
                                case "BATON":
                                    mage.AjouterEquipement(baton);
                                    Console.WriteLine("bâton ajouté");
                                    choixEquip = " ";
                                    break;
                                case "ARC":
                                    mage.AjouterEquipement(arc);
                                    Console.WriteLine("arc ajouté");
                                    choixEquip = " ";
                                    break;
                                case "RIEN":
                                    Console.WriteLine("Ah VRAIMENT");
                                    choixEquip = " ";
                                    break;
                            }
                        }
                        Console.WriteLine(nomM + " a bien rejoint notre équipe \r\n");
                        Console.WriteLine(mage);
                        break;
                    case "ARCHER":
                        string nomA = "";
                        do
                        {
                            Console.WriteLine("Un archer rejoint les rangs !! Comment va-t-on l'appeler ?");
                            nomA = Console.ReadLine();
                        } while (nomA == "");

                        Archer archer = new Archer(nomA);
                        ListTeam.Add(archer);
                        Console.WriteLine("Bienvenue " + nomA);
                        do
                        {
                            Console.WriteLine("voulez-vous lui donner une pièce d'armure? oui/non");
                            choix = Console.ReadLine().ToUpper();
                        } while (choix != "OUI" && choix != "NON");

                        if (choix == "OUI")
                        {
                            while ((choixEquip != "CHAUSSON") && (choixEquip != "SLIP") && (choixEquip != "RIEN"))
                            {
                                Console.WriteLine("que voulez-vous lui donner ? chausson / slip / rien");
                                choixEquip = Console.ReadLine().ToUpper();
                            }

                            switch (choixEquip)
                            {
                                case "CHAUSSON":
                                    archer.AjouterEquipement(chausson);
                                    Console.WriteLine("Chausson ajouté sur " + nomA);
                                    choixEquip = " ";
                                    break;
                                case "SLIP":
                                    archer.AjouterEquipement(slip);
                                    Console.WriteLine("Slip ajouté sur " + nomA);
                                    choixEquip = " ";
                                    break;
                                case "RIEN":
                                    Console.WriteLine("Ah VRAIMENT");
                                    choixEquip = " ";
                                    break;
                            }
                        }
                        choix = " ";
                        do
                        {
                            Console.WriteLine("Voulez-vous donner une arme ? oui/non");
                            choix = Console.ReadLine().ToUpper();
                        } while (choix != "OUI" && choix != "NON");

                        if (choix == "OUI")
                        {
                            while (choixEquip != "EPEE" && choixEquip != "BATON" && choixEquip != "ARC" && choixEquip != "RIEN")
                            {
                                Console.WriteLine("que voulez-vous lui donner ? epee / baton / arc / rien");
                                choixEquip = Console.ReadLine().ToUpper();
                            }

                            switch (choixEquip)
                            {
                                case "EPEE":
                                    archer.AjouterEquipement(epee);
                                    Console.WriteLine("épée ajouté");
                                    choixEquip = " ";
                                    break;
                                case "BATON":
                                    archer.AjouterEquipement(baton);
                                    Console.WriteLine("bâton ajouté");
                                    choixEquip = " ";
                                    break;
                                case "ARC":
                                    archer.AjouterEquipement(arc);
                                    Console.WriteLine("arc ajouté");
                                    choixEquip = " ";
                                    break;
                                case "RIEN":
                                    Console.WriteLine("Ah VRAIMENT");
                                    choixEquip = " ";
                                    break;
                            }
                        }
                        Console.WriteLine(nomA + " a bien rejoint notre équipe \r\n");
                        Console.WriteLine(archer);
                        break;
                }
            }
            return ListTeam;
        }    

    }
}