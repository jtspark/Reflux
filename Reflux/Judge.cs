namespace Reflux
{
    class Judge
    {
        public PlayType playtype;
        public int pgreat;
        public int great;
        public int good;
        public int bad;
        public int poor;
        public int fast;
        public int slow;
        public int combobreak;
        public bool prematureEnd;
        public bool quickRetry;

        public bool PFC { get { return (good + bad + poor) == 0; } }
        /// <summary>
        /// Fetch all the judge information from the play
        /// </summary>
        public void Fetch()
        {
            short word = 4;

            FetchPlayType();
            var p1pgreat = Utils.ReadInt32(Offsets.JudgeData, 0);
            var p1great = Utils.ReadInt32(Offsets.JudgeData, word);
            var p1good = Utils.ReadInt32(Offsets.JudgeData, word * 2);
            var p1bad = Utils.ReadInt32(Offsets.JudgeData, word * 3);
            var p1poor = Utils.ReadInt32(Offsets.JudgeData, word * 4);
            var p2pgreat = Utils.ReadInt32(Offsets.JudgeData, word * 5);
            var p2great = Utils.ReadInt32(Offsets.JudgeData, word * 6);
            var p2good = Utils.ReadInt32(Offsets.JudgeData, word * 7);
            var p2bad = Utils.ReadInt32(Offsets.JudgeData, word * 8);
            var p2poor = Utils.ReadInt32(Offsets.JudgeData, word * 9);
            var p1cb = Utils.ReadInt32(Offsets.JudgeData, word * 10);
            var p2cb = Utils.ReadInt32(Offsets.JudgeData, word * 11);
            var p1fast = Utils.ReadInt32(Offsets.JudgeData, word * 12);
            var p2fast = Utils.ReadInt32(Offsets.JudgeData, word * 13);
            var p1slow = Utils.ReadInt32(Offsets.JudgeData, word * 14);
            var p2slow = Utils.ReadInt32(Offsets.JudgeData, word * 15);
            var p1measure_end = Utils.ReadInt32(Offsets.JudgeData, word * 16);
            var p2measure_end = Utils.ReadInt32(Offsets.JudgeData, word * 17);

            pgreat = p1pgreat + p2pgreat;
            great = p1great + p2great;
            good = p1good + p2good;
            bad = p1bad + p2bad;
            poor = p1poor + p2poor;
            fast = p1fast + p2fast;
            slow = p1slow + p2slow;
            combobreak = p1cb + p2cb;
            prematureEnd = p1measure_end + p2measure_end != 0;

            quickRetry = pgreat + great + good + bad + poor == 0 && !prematureEnd;
        }

        public void FetchPlayType()
        {
            short word = 4;
            short mode_offset = 1239;
            short side_offset_P1 = 1287;
            //short side_offset_P2 = 1288;

            // mode: 1 = Single, 2 = Double
            var mode = Utils.ReadInt32(Offsets.PlayData, word * mode_offset);
            // side: 1 = P1, 0 = P2 (only in Single mode)
            var side = Utils.ReadInt32(Offsets.PlayData, word * side_offset_P1);

            playtype = mode == 1
                ? side == 1 ? PlayType.P1 : PlayType.P2
                : PlayType.DP;
        }
    }
}
