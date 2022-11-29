Module Module1

    Public Enum TileState
        Empty
        Miss
        Hit
        CompShip
        UserShip
        CompShot
    End Enum



    Public rows As Integer = 4
    Public columns As Integer = 5
    'this 2d array will be used to represent the board
    Public board(rows - 1, columns - 1) As TileState



    Sub Main()
        Dim gameOver As Boolean = False
        Dim shotcount As Integer = 0
        ResetBoard()
        placeUserShip()
        PlaceComputerShip()
        Do
            shotcount += 1
            PrintBoard()
            Dim valid As Boolean = False
            Do
                Dim row As Integer = GetUserInput("Please enter the row to fire on (0-" & rows - 1 & ") -> ", rows - 1)
                Dim col As Integer = GetUserInput("Please enter the col to fire on (0-" & columns - 1 & ") -> ", columns - 1)
                If board(row, col) = TileState.CompShip Then
                    board(row, col) = TileState.Hit
                    gameOver = True
                    valid = True
                    Console.WriteLine("You hit it in {0} shots!", shotcount.ToString)
                ElseIf board(row, col) = TileState.UserShip Then
                    Console.WriteLine("Don't fire on yourself!")
                    Console.WriteLine("Try again.")
                    GetUserInput("Please enter the row to fire on (0-3) -> ", rows - 1)
                    GetUserInput("Please enter the col to fire on (0-4) -> ", columns - 1)
                Else
                    board(row, col) = TileState.Miss
                    valid = True
                End If
            Loop While Not valid
            If Not gameOver Then
                gameOver = ComputerAttack()
            End If
        Loop While Not gameOver
        PrintBoard()
    End Sub
    ''' <summary>
    ''' Sets all times to TileState.empty
    ''' </summary>
    Sub ResetBoard()
        For i As Integer = 0 To rows - 1
            For x As Integer = 0 To columns - 1
                board(i, x) = TileState.Empty
            Next
        Next
    End Sub


    ''' <summary>
    '''selects a random int from 0 to rows - 1 and random int from columns - 1 to
    '''place a computer ship
    ''' </summary>
    Sub PlaceComputerShip()
        Dim valid = True
        Do
            Dim rand1 As New Random
            Dim row As Integer = rand1.Next(0, rows)
            Dim column As Integer = rand1.Next(0, columns)
            Select Case board(row, column)
                Case TileState.UserShip
                    valid = False
                Case TileState.Empty
                    board(row, column) = TileState.CompShip
                    valid = True
            End Select
        Loop While valid = False
    End Sub
    ' has the computer attack on a tile that hasnt been attacked yet, or is the comp ship
    Function ComputerAttack() As Boolean
        Dim valid As Boolean = True
        Dim gameover As Boolean = False
        Do
            Dim rand1 As New Random
            Dim row As Integer = rand1.Next(0, rows)
            Dim column As Integer = rand1.Next(0, columns)
            Select Case board(row, column)
                Case TileState.Miss
                    valid = False
                Case TileState.CompShip
                    valid = False
                Case TileState.UserShip
                    board(row, column) = TileState.Hit
                    valid = True
                    gameover = True
                Case TileState.Empty
                    board(row, column) = TileState.CompShot
                    valid = True
            End Select
        Loop While Not valid
        Return gameover
    End Function
    'prompts the user to place thier ship
    Sub placeUserShip()
        Dim row As Integer = GetUserInput("Please enter the row to place your ship on (0-" & rows - 1 & ") -> ", rows - 1)
        Dim col As Integer = GetUserInput("Please enter the col to place your ship on (0-" & columns - 1 & ") -> ", columns - 1)
        board(row, col) = TileState.UserShip
    End Sub
    ''' <summary>
    ''' prints the board
    ''' if it is empty put a dash
    ''' if miss print x
    ''' if hit print h
    ''' </summary>
    Sub PrintBoard()
        Console.Write("     ")
        For col As Integer = 0 To columns - 1
            Console.Write(col & "   ")
        Next
        Console.WriteLine()
        Console.WriteLine("   ---------------------")
        For row As Integer = 0 To board.GetUpperBound(0)
            Console.Write(" " & row & " |")
            For col As Integer = 0 To board.GetUpperBound(1)
                Select Case board(row, col)
                    Case TileState.Empty, TileState.CompShip
                        Console.ForegroundColor = ConsoleColor.Blue
                        Console.Write(" -".PadRight(3))
                        Console.ForegroundColor = ConsoleColor.White
                        Console.Write("|")
                    Case TileState.Miss
                        Console.ForegroundColor = ConsoleColor.Red
                        Console.Write(" X".PadRight(3))
                        Console.ForegroundColor = ConsoleColor.White
                        Console.Write("|")
                    Case TileState.Hit
                        Console.ForegroundColor = ConsoleColor.Green
                        Console.Write(" H".PadRight(3))
                        Console.ForegroundColor = ConsoleColor.White
                        Console.Write("|")
                    Case TileState.UserShip
                        Console.ForegroundColor = ConsoleColor.Yellow
                        Console.Write(" U".PadRight(3))
                        Console.ForegroundColor = ConsoleColor.White
                        Console.Write("|")
                    Case TileState.CompShot
                        Console.ForegroundColor = ConsoleColor.DarkRed
                        Console.Write(" C".PadRight(3))
                        Console.ForegroundColor = ConsoleColor.White
                        Console.Write("|")
                End Select
            Next
            Console.WriteLine()
            Console.WriteLine("   ---------------------")
        Next
    End Sub
    ''' <summary>
    ''' repeats the prompt to the user until a number between 0 and max is given
    ''' returns that number
    ''' </summary>
    ''' <param name="prompt"></param>
    ''' <param name="max"></param>
    ''' <returns></returns>
    Function GetUserInput(prompt As String, max As Integer)
        Dim valid As Boolean = False
        Dim inputStr As String
        Dim userInput As Integer
        'ask the user for input until valid input is given
        Do
            Console.Write(prompt)
            inputStr = Console.ReadLine
            valid = Integer.TryParse(inputStr, userInput)
            If Not (valid AndAlso userInput >= 0 AndAlso userInput <= max) Then
                valid = False
            End If
        Loop While Not valid
        Return userInput
    End Function



End Module
