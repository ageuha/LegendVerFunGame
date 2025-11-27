using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.AI.DQN {
    public class ReplayBuffer {
        private readonly Queue<Experience> _buffer;
        private readonly int _maxSize;

        public ReplayBuffer(int maxSize) {
            _maxSize = maxSize;
            _buffer = new Queue<Experience>(maxSize);
        }

        public void Add(Experience experience) {
            if (_buffer.Count >= _maxSize) {
                _buffer.Dequeue();
            }
            _buffer.Enqueue(experience);
        }

        public Experience[] SampleBatch(int batchSize) {
            if (_buffer.Count < batchSize) {
                return _buffer.ToArray();
            }

            var shuffled = _buffer.ToArray().OrderBy(x => Random.Range(0f, 1f)).ToArray();
            return shuffled.Take(batchSize).ToArray();
        }

        public int Count => _buffer.Count;
        public bool CanSample(int batchSize) => _buffer.Count >= batchSize;
    }
}