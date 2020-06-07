using UnityEngine;

public class NoteNameDetector
{
    private string[] noteNames = { "ド", "ド♯", "レ", "レ♯", "ミ", "ファ", "ファ♯", "ソ", "ソ♯", "ラ", "ラ♯", "シ", ""};

    public string GetNoteName(float freq)
    {
        // 周波数からMIDIノートナンバーを計算
        var noteNumber = calculateNoteNumberFromFrequency(freq);
        if (noteNumber == 12)
        {
            return noteNames[12];
        }else
        {
            // 0:C - 11:B に収める
            var note = noteNumber % 12;
            // 0:C～11:Bに該当する音名を返す
            //Debug.Log("Note" + note);
            return noteNames[note];
        }
        
    }

    // See https://en.wikipedia.org/wiki/MIDI_tuning_standard
    private int calculateNoteNumberFromFrequency(float freq)
    {
        int freqNote = Mathf.FloorToInt(69 + 12 * Mathf.Log(freq / 440, 2));
        
        if (freq <= 0)
        {
            Debug.Log("Note" + freqNote);
            return 12;

        }else
        {
            return freqNote;
        }

        
    }
}
