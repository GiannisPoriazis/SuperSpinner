namespace SuperSpinner.Audio
{
    public interface IAudioManager
    {
        /// <summary>
        /// Plays a click sound with randomized pitch variation.
        /// </summary>
        void PlayClickSound();

        /// <summary>
        /// Plays a prize sound with pitch scaled based on the prize value.
        /// </summary>
        /// <param name="prize">The prize value used to determine the pitch of the sound.</param>
        void PlayPrizeSound(long prize);
    }
}
