using Microsoft.EntityFrameworkCore;
using NotesApi.Data;
using NotesApi.Models;

namespace NotesApi.Repositories;

public class NotesRepository : INotesRepository
{
    private readonly NotesDbContext _context;

    public NotesRepository(NotesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await _context.Notes
            .OrderByDescending(n => n.Timestamp)
            .ToListAsync();
    }

    public async Task<Note?> GetNoteByIdAsync(int id)
    {
        return await _context.Notes.FindAsync(id);
    }

    public async Task<Note> CreateNoteAsync(Note note)
    {
        note.Timestamp = DateTime.UtcNow;
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
        return note;
    }

    public async Task<Note?> UpdateNoteAsync(int id, Note note)
    {
        var existingNote = await _context.Notes.FindAsync(id);
        if (existingNote == null)
        {
            return null;
        }

        existingNote.Title = note.Title;
        existingNote.Content = note.Content;
        existingNote.Timestamp = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existingNote;
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null)
        {
            return false;
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
        return true;
    }
} 