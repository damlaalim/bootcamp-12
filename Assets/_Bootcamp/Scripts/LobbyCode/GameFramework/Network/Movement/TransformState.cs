using System;
using Unity.Netcode;
using UnityEngine;

namespace _Bootcamp.Scripts.LobbyCode.GameFramework.Network.Movement
{
    public class TransformState : INetworkSerializable, IEquatable<TransformState>
    {
        public int Tick;
        public Vector3 Position;
        public Quaternion Rotation;
        public bool HasStartedMoving;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            if (serializer.IsReader)
            {
                var reader = serializer.GetFastBufferReader();
                reader.ReadValueSafe(out Tick);
                reader.ReadValueSafe(out Position);
                reader.ReadValueSafe(out Rotation);
                reader.ReadValueSafe(out HasStartedMoving);
                
            }
            else
            {
                var writer = serializer.GetFastBufferWriter();
                writer.WriteValueSafe(Tick);
                writer.WriteValueSafe(Position);
                writer.WriteValueSafe(Rotation);
                writer.WriteValueSafe(HasStartedMoving);
            }

        }
        
        public bool Equals(TransformState other)
        {
            if (other == null) return false;
            return Tick == other.Tick &&
                   Position.Equals(other.Position) &&
                   Rotation.Equals(other.Rotation) &&
                   HasStartedMoving == other.HasStartedMoving;
        }

        public override bool Equals(object obj)
        {
            if (obj is TransformState other)
            {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Tick.GetHashCode();
                hash = hash * 23 + Position.GetHashCode();
                hash = hash * 23 + Rotation.GetHashCode();
                hash = hash * 23 + HasStartedMoving.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(TransformState left, TransformState right)
        {
            if (ReferenceEquals(left, null)) return ReferenceEquals(right, null);
            return left.Equals(right);
        }

        public static bool operator !=(TransformState left, TransformState right)
        {
            return !(left == right);
        }
    }
}