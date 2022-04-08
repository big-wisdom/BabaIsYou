using Systems;

namespace Components
{
    class Word : Component
    {
        public Words word;
        public Word(Words word)
        {
            this.word = word;
        }
    }
}
