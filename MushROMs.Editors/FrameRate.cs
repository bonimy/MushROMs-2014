
namespace MushROMs.Editors
{
    #region Enumerations
    /// <summary>
	/// Specifies constants defining particular TV frame rates.
	/// </summary>
	public enum FPSModes
	{
		/// <summary>
		/// National Television Systems Committee. Runs at 60 FPS.
		/// </summary>
		NTSC = 60,
		/// <summary>
		/// Phase Alternating Line. Runs at 50 FPS.
		/// </summary>
		PAL = 50
	}

	/// <summary>
	/// Specifies constants defining a fractional amount of frame reduction
    /// the editor's animator should have. Useful for running on computers
    /// with less processing power.
	/// </summary>
	public enum FrameReductions
	{
		/// <summary>
		/// Do not reduce frame rate.
		/// </summary>
		None = 1,
		/// <summary>
		/// Reduce frame rate by half of its usual value.
		/// </summary>
		Half = 2,
		/// <summary>
		/// Reduce frame rate by a fourth of its usual value.
		/// </summary>
		Fourth = 4,
		/// <summary>
		/// Reduce frame rate by an eighth of its usual value.
		/// </summary>
		Eighth = 8
	}
    #endregion
}
