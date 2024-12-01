using System.Text;

public interface ICommand
{
    void Execute();
    void Undo();
}
public class InsertTextCommand : ICommand
{
    private readonly TextEditor _editor;
    private readonly string _text;
    private int _position;

    public InsertTextCommand(TextEditor editor, string text, int position)
    {
        _editor = editor;
        _text = text;
        _position = position;
    }

    public void Execute()
    {
        _editor.InsertText(_text, _position);
    }

    public void Undo()
    {
        _editor.DeleteText(_position, _text.Length);
    }
}
public class DeleteTextCommand : ICommand
{
    private readonly TextEditor _editor;
    private readonly int _position;
    private readonly int _length;
    private string _deletedText;

    public DeleteTextCommand(TextEditor editor, int position, int length)
    {
        _editor = editor;
        _position = position;
        _length = length;
    }

    public void Execute()
    {
        _deletedText = _editor.DeleteText(_position, _length);
    }

    public void Undo()
    {
        _editor.InsertText(_deletedText, _position);
    }
}
public class TextEditor
{
    private StringBuilder _text = new StringBuilder();

    public string Text => _text.ToString();

    public void InsertText(string text, int position)
    {
        _text.Insert(position, text);
    }

    public string DeleteText(int position, int length)
    {
        var deletedText = _text.ToString(position, length);
        _text.Remove(position, length);
        return deletedText;
    }
}
public class CommandHistory
{
    private readonly Stack<ICommand> _history = new Stack<ICommand>();

    public void Push(ICommand command)
    {
        _history.Push(command);
    }

    public ICommand Pop()
    {
        return _history.Count > 0 ? _history.Pop() : null;
    }
}
class Program
{
    static void Main(string[] args)
    {
        var editor = new TextEditor();
        var history = new CommandHistory();

        // Ввод текста
        var insertCommand1 = new InsertTextCommand(editor, "Hello, ", 0);
        insertCommand1.Execute();
        history.Push(insertCommand1);

        var insertCommand2 = new InsertTextCommand(editor, "world!", 7);
        insertCommand2.Execute();
        history.Push(insertCommand2);

        Console.WriteLine(editor.Text); // Вывод: Hello, world!

        // Удаление текста
        var deleteCommand = new DeleteTextCommand(editor, 5, 2);
        deleteCommand.Execute();
        history.Push(deleteCommand);

        Console.WriteLine(editor.Text); // Вывод: Hello world!

        // Отмена последней операции
        var lastCommand = history.Pop();
        if (lastCommand != null)
        {
            lastCommand.Undo();
        }

        Console.WriteLine(editor.Text); // Вывод: Hello, world!
    }
}