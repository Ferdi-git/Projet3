using System;
using System.Collections.Generic;

[Serializable]
public class Context 
{
    public int Tour;

    public int NbrCaseDeLaPiece;

    public int NbrPiecesAutour;
    public int NbrCaseLibre;
    public int NbrCaseOccupé;

    public int NbrDeRepetition;

    // script de stats joueur 
    // script de stats ennemi 

    public List<BoardPiece> voisins;




}
