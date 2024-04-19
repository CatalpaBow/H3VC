namespace H3VC.Data
{
    /// <summary>
    /// pcm audio codec data
    /// </summary>
    public class PCMSegment
    {
        readonly public float[] pcmBuffer;
        readonly public int pcmLength;
        public PCMSegment(float[] _pcmBuffer, int _pcmLength) {
            pcmBuffer = _pcmBuffer;
            pcmLength = _pcmLength;
        }
    }
}
