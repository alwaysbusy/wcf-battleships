using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BattleshipsLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace = "http://com.hurfo.wcfbattle")]
    public interface IBattleships
    {
        [OperationContract]
        int Attack(int player, int x, int y); // 0=Miss, 1=Hit, 2=Destroyed, 3=Win

        [OperationContract]
        int CurrentTurn();

        [OperationContract]
        void CreateGrid(int player, int[] grid);

        [OperationContract]
        int AddPlayer();

        [OperationContract]
        Tuple<int, int[]> GameState();
    }
}
