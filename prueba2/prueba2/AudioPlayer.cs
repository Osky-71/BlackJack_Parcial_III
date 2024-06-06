using System;
using System.Media;

public class AudioPlayer
{
    private SoundPlayer player;

    public AudioPlayer(string soundFilePath)
    {
        if (OperatingSystem.IsWindows())
        {
            player = new SoundPlayer(soundFilePath);
        }
    }

    public void PlaySound()
    {
        if (OperatingSystem.IsWindows())
        {
            try
            {
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al reproducir el sonido: {ex.Message}");
            }
        }
    }

    public void StopSound()
    {
        if (OperatingSystem.IsWindows())
        {
            try
            {
                player.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al detener el sonido: {ex.Message}");
            }
        }
    }
}
