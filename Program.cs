using System.Runtime.CompilerServices;

namespace Main;

public class Board {
    static Random rng  = new Random();
    int[] node = {1,2,3,4,5,6,7,8,0};
    int [] Table_lookUp = {1,2,3,4,5,6,7,8,0};

    Dictionary<int,List<int>> Avalible_move = new Dictionary<int, List<int>>{
        {0, new List<int>{1,3}},
        {1, new List<int>{0,2,4}},
        {2, new List<int>{1,5}},
        
        {3, new List<int>{0,4,6}},
        {4, new List<int>{1,3,5,7}},
        {5, new List<int>{2,4,8}},
        
        {6, new List<int>{7,3}},
        {7, new List<int>{6,8,4}},
        {8, new List<int>{7,5}},
    };
    
    public string Print_Board(){

        return String.Format(" |0 {0} |1 {1} |2 {2} | \n |3 {3} |4 {4} |5 {5} | \n |6 {6} |7 {7} |8 {8} |"
                            ,node[0],node [1], node [2],node [3], node [4] ,node [5], node [6], node [7] , node[8]);
    }

    public void Suffle_Data(){
        for (int i = 0; i < node.Length -1 ; i ++){
            int random_index = rng.Next(1,9);
            int tmp = node[i];
            node[i] = node [random_index];
            node [random_index] = tmp;
            
        }
        
    }
    public void Swap_data (int old_index , int new_index){
        int temp = node[old_index];
        node [old_index] = node [new_index];
        node [new_index] = temp;
    }

    public bool Is_win(){
        return node.SequenceEqual(Table_lookUp);
    }

    public void Move (int index){
        int new_index = index;
        var index_value_nol = Array.IndexOf(this.node, 0);
        int is_avalibel = Avalible_move.First(i => i.Key == index_value_nol).Key;
        if (Avalible_move[is_avalibel].Contains(index)){
            this.Swap_data(index_value_nol, index);
        }else{
            throw new ArgumentException(String.Format("Move {0} Is Invalid",index));
        }
    }

    public void Edit_board(){
        this.node = new int []{1,2,3,4,5,6,7,8,0};
    }

}

[Flags]
public enum Game_State{
    Playing = 0,
    Quit = 1
}

public class Game{

    Board _Board = new Board();

    Game_State state ;
    public Game () {
        _Board.Suffle_Data();
        state = 0;
     
    }

    public void GameLoop (){
        while (state == 0){
            Console.Clear();
            Console.WriteLine(_Board.Print_Board());
            Console.Write("Your Move : " );try{
            int move_index = Convert.ToInt16(Console.ReadLine());
            _Board.Move(move_index);
            //_Board.Edit_board();
            }catch(FormatException){
                Console.WriteLine("Input Is Invalid");
            }catch(ArgumentException e){
                Console.Write("Move Is Invalid {0}",e);
            }catch(InvalidOperationException e ){
                Console.Write("Move Is Invalid {0}",e);
            }
            if (_Board.Is_win()){
                Console.Write("You Win !!!!!");
                state = (Game_State)1;
            }
        }
    }
}


class Program {

    static void Main (){
        Game game = new Game();
        game.GameLoop();
    }
}
