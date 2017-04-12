using System;

[Serializable]
public enum HexDirection
{
    // Orientation: ⬡    ⬣
    One,   //...... NE    N
    Two,   //...... E     NE
    Three, //...... SE    SE
    Four,  //...... SW    S
    Five,  //...... W     SW
    Six,   //...... NW    NW
}