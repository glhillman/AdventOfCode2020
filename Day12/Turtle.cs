using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    public class Instruction
    {
        public Instruction(string txtInstruction)
        {
            Command = txtInstruction[0];
            Value = int.Parse(txtInstruction.Substring(1));
        }

        public char Command { get; private set; }
        public int  Value { get; private set; }
        public override string ToString()
        {
            return string.Format("Cmd: {0}, Value: {1}", Command, Value);
        }
    }
    public class Turtle
    {
        public Turtle(int x, int y)
        {
            ShipX = 0;
            ShipY = 0;
            WaypointX = x;
            WaypointY = y;
            CurrentCompass = 0;
        }

        public void Cmd1(Instruction instruction)
        {
            switch (instruction.Command)
            {
                case 'N': 
                    ShipY += instruction.Value;
                    break;
                case 'S': 
                    ShipY -= instruction.Value;
                    break;
                case 'E':
                    ShipX += instruction.Value;
                    break;
                case 'W':
                    ShipX -= instruction.Value;
                    break;
                case 'L':
                    CurrentCompass = (CurrentCompass + instruction.Value) % 360;
                    break;
                case 'R':
                    CurrentCompass = CurrentCompass - instruction.Value;
                    if (CurrentCompass < 0)
                    {
                        CurrentCompass += 360;
                    }
                    break;
                case 'F':
                    switch (CurrentCompass)
                    {
                        case 0:
                            ShipX += instruction.Value;
                            break;
                        case 90:
                            ShipY += instruction.Value;
                            break;
                        case 180:
                            ShipX -= instruction.Value;
                            break;
                        case 270:
                            ShipY -= instruction.Value;
                            break;
                        default:
                            Console.WriteLine("Yikes!");
                            break;
                    }
                    break;
                default:
                    throw new ArgumentException(string.Format("Unexpected Command: {0}", instruction.Command));
            }
        }

        public void Cmd2(Instruction instruction)
        {
            switch (instruction.Command)
            {
                case 'N':
                    WaypointY += instruction.Value;
                    break;
                case 'S':
                    WaypointY -= instruction.Value;
                    break;
                case 'E':
                    WaypointX += instruction.Value;
                    break;
                case 'W':
                    WaypointX -= instruction.Value;
                    break;
                case 'L':
                    RotateWaypoint(instruction.Value);
                    break;
                case 'R':
                    RotateWaypoint(instruction.Value * -1);
                    break;
                case 'F':
                    double deltaX = WaypointX - ShipX;
                    double deltaY = WaypointY - ShipY;
                    ShipX += deltaX * instruction.Value;
                    ShipY += deltaY * instruction.Value;
                    WaypointX = ShipX + deltaX;
                    WaypointY = ShipY + deltaY;
                    break;
                default:
                    throw new ArgumentException(string.Format("Unexpected Command: {0}", instruction.Command));
            }
        }

        public double ManhattanDistance
        {
            get { return Math.Abs(ShipX) + Math.Abs(ShipY); }
        }

        private void RotateWaypoint(double angle)
        {
            double sin = Math.Sin(angle * (Math.PI / 180.0));
            double cos = Math.Cos(angle * (Math.PI / 180.0));
            // translate point back to origin:
            WaypointX -= ShipX;
            WaypointY -= ShipY;
            // rotate point
            double newX = Math.Round(WaypointX * cos - WaypointY * sin);
            double newY = Math.Round(WaypointX * sin + WaypointY * cos);
            // translate point back:
            WaypointX = newX + ShipX;
            WaypointY = newY + ShipY;
        }

        public double ShipX { get; private set; }
        public double ShipY { get; private set; }
        public double WaypointX { get; private set; }
        public double WaypointY { get; private set; }
        public int CurrentCompass { get; private set; }
        public override string ToString()
        {
            return string.Format("Ship x,y: {0},{1}, Waypoint x,y {2},{3}, Compass: {2}", ShipX, ShipY, WaypointX, WaypointY, CurrentCompass);
        }
    }
}
