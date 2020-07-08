using System;
using TestTasks.Robots.Contracts;

namespace TestTasks.Robots
{
    public class SimpleRobot : IRobot
    {
        private Orientation orientation = Orientation.N;
        private Position lastPosition = Position.Origin;

        public IArea Area { get; set; }

        public Orientation Orientation
        {
            get => orientation;
            set
            {
                if (Status == Status.Lost)
                    return;

                orientation = value;
            }
        }

        public Position LastPosition
        {
            get => lastPosition;
            set
            {
                if (Area == null)
                    throw new InvalidOperationException("Can not set position without area.");

                if (Status == Status.Lost)
                    return;

                if (Area.ScentPosition.Contains(value))
                    return;

                if (Area.Contains(value))
                {
                    lastPosition = value;
                }
                else
                {
                    Status = Status.Lost;
                    Area.ScentPosition.Add(value);
                }
            }
        }

        public Status Status { get; private set; } = Status.Normal;

        public override string ToString() =>
            $"{{Position: {LastPosition}; Orientation: {Orientation}; Status: {Status}}}";
    }
}