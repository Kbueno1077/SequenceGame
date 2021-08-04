using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GameCode
{
  public class Connection
    {
        private List<Player> players;


        //Cargar el fichero, constructor de la clase
        public Connection()
        {
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    players = (List<Player>)bin.Deserialize(stream);
                }
            }
            catch (FileNotFoundException)
            {
                players = new List<Player>();
            }
            catch (IOException) {

            }
        }

        public List<Player> SortByGame(string game) {
            switch (game) {
                case ("Luces"): {
                        players.Sort((x,y) => y.LucesMaxScore.CompareTo(x.LucesMaxScore));
                  }break;
            }
            return players;
        }

        //Not in Use at the moment, borrar todos los Usuarios
        public void DeletePlayers() {
            for (int i = 0; i < players.Count; i++) {
                players.RemoveAt(0);
                WriteInFile();
            }
        }

            
        //Not in Use at the moment borar todas las Estadisticas del Usuario Actual
        public void DeleteStats() {
            foreach (Player player in players) {
                if (player.Name.Equals(PlayerCache.Usuario)) {
                    player.LucesMaxScore = 0;
                    //mas estadisticas
                    WriteInFile();
                    break;
                }
            }
        }

        //Not in Use at the moment borar todas las Estadisticas de un Usuario en un juego
        public void DeleteStatsOf(string game)
        {
            foreach (Player player in players)
            {

                if (player.Name.Equals(PlayerCache.Usuario))
                {
                    switch (game)
                    {
                        case ("Luces"):
                            {
                                player.LucesMaxScore = 0;
                            }
                            break;
                    }
                    
                    WriteInFile();
                    break;
                }
            }
        }

        //Cargar datos del Jugador Actual
        public bool Connect(String user, String pass, string ModeOfGame) {

        if (ModeOfGame.Equals("Normal"))
            {
                for (int i = 0; i < players.Count; i++)
                {
                    Player p = players.ElementAt(i);
                    if (p.Name.Equals(user) && p.Password.Equals(pass))
                    {
                        PlayerCache.Usuario = p.Name;
                        PlayerCache.Password = p.Password;
                        PlayerCache.LucesMaxScore = p.LucesMaxScore;
                        PlayerCache.ModeOfGame = "Normal";
                        return true;
                    }
                }
            }
            else
            {
                PlayerCache.ModeOfGame = "NoLogin";
                return true;
            }
                return false;
        }

        //Añadir Usuario
        public bool AddPlayer(Player newPlayer) {

            for (int i = 0; i < players.Count; i++) {
                if (players.ElementAt(i).Name == newPlayer.Name)
                    return false;
               }
                players.Add(newPlayer);
                WriteInFile();
                return true;
                }
        //editar un Usuario
        public void EditPlayer(Player editPlayer, String user, String pass) {
            for (int i = 0; i < players.Count; i++)
            {
                Player p = players.ElementAt(i);
                if (p.Name.Equals(user) && p.Password.Equals(pass))
                {
                    p.Name = editPlayer.Name;
                    p.Password = editPlayer.Password;
                    PlayerCache.Usuario = editPlayer.Name;
                    PlayerCache.Password = editPlayer.Password;

                }
            }
                    WriteInFile();
        }

        //Actualizar el MaxScore de algun juego
        public void UpdateScore(string Game, int MaxScore) {
            switch (Game) {
                case ("Luces"): {
                        for (int i = 0; i < players.Count; i++)
                        {
                            Player p = players.ElementAt(i);
                            if (p.Name.Equals(PlayerCache.Usuario) && p.Password.Equals(PlayerCache.Password)) {
                                p.LucesMaxScore = MaxScore;
                            }
                          }
                        }
                        break;
                     }
                WriteInFile();
             }

        //Guardar en el fichero, los usuarios con sus Scores
        private void WriteInFile() {
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, players);
                   
                }
            }
            catch (IOException) { }
        }


    }
}
