using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class BigInteger
{
    private List<byte> list;
    public BigInteger(List<byte>value)
    {
        this.list  = new List<byte>(value);
    }
    public BigInteger (): this (new List<byte>()){      
    }
    public byte getByte(int index) { 
       return list.ElementAt(index);
    }
    public  void setHex(string value)
    {
        list = new List<byte>();
        string tmp = "0" + value;
        int length = tmp.Length;
        for (int i = length-2; i >= 0; i-=2)
        {
            list.Add(Convert.ToByte(tmp.Substring(i, 2), 16));
        }
        foreach (byte b in list)
        {
            Console.Write(b.ToString()+ " ");
        }
    }
    public string getHex()
    {
        return Convert.ToHexString(list.Reverse<byte>().ToArray());
    }
    public List<byte> getBytes()
    {
        return new List<byte>(this.list);
    }
    public BigInteger XOR(BigInteger num1, BigInteger num2)
    {
        List<byte> resultBytes = new List<byte>();
        for (int i = 0; i < num1.getBytes().Count; i++)
        {
            resultBytes.Add((byte)(num1.getByte(i) ^ num2.getByte(i))); 
        }
        return new BigInteger(resultBytes);
    }
    public BigInteger INV(BigInteger num1) 
    {
        List<byte> resultBytes = new List<byte>();
        for (int i = 0; i < num1.getBytes().Count; i++)
        {
            resultBytes.Add((byte)(~num1.getByte(i)));
        }
        return new BigInteger(resultBytes);
    }
    public BigInteger OR(BigInteger num1, BigInteger num2)
    {
        List<byte> resultBytes = new List<byte>();
        for (int i = 0; i < num1.getBytes().Count; i++)
        {
            resultBytes.Add((byte)(num1.getByte(i) | num2.getByte(i)));
        }
        return new BigInteger(resultBytes);
    }
    public BigInteger AND(BigInteger num1, BigInteger num2)
    {
        List<byte> resultBytes = new List<byte>();
        for (int i = 0; i < num1.getBytes().Count; i++)
        {
            resultBytes.Add((byte)(num1.getByte(i) & num2.getByte(i)));
        }
        return new BigInteger(resultBytes);

    }
    public BigInteger ShiftRight(BigInteger num1, int n)
    {
        List<byte> resultBytes = new List<byte>();
        for (int i = 0; i < num1.getBytes().Count; i++)
        {
            resultBytes.Add((byte)(num1.getByte(i)));
        }

        if (n >= 8)
        {
            return new BigInteger(resultBytes); 
        }
        byte mask = (byte)((1 << n) - 1);
        byte carry = 0;
        byte nextCarry = 0;
        for (int i = 0; i < resultBytes.Count; i++)
        {
            nextCarry = (byte)((resultBytes[i] & mask) << (8-n));
            resultBytes[i] = (byte)((resultBytes[i] >> n) | carry);
            carry = nextCarry;
        }
        return new BigInteger(resultBytes);
    }
    public BigInteger ShiftLeft(BigInteger num1, int n)
    {
        List<byte> resultBytes = new List<byte>();
        for (int i = 0; i < num1.getBytes().Count; i++)
        {
            resultBytes.Add((byte)(num1.getByte(i)));
        }
        if (n >= 8)
        {
            return new BigInteger(resultBytes);
        }
        byte mask = (byte)(((1 << n) - 1)<<(8-n));
        byte carry = 0;
        byte nextCarry = 0;
        for (int i = resultBytes.Count-1; i >= 0; i--)
        {
            nextCarry = (byte)((resultBytes[i] & mask) >> (8 - n));
            resultBytes[i] = (byte)((resultBytes[i] << n) | carry);
            carry = nextCarry;
        }
        return new BigInteger(resultBytes);
    }
    public BigInteger Add(BigInteger num1, BigInteger num2)
    {
        List<byte> resultBytes = new List<byte>();
        int tmp = 0; 
        int maxLength = Math.Max(num1.getBytes().Count, num2.getBytes().Count);

        for (int i = 0; i < maxLength || tmp > 0; i++)
        {
            int sum = tmp;
            if (i < (num1.getBytes().Count))
            {
                sum += num1.getBytes()[i];
            }
            if (i < num2.getBytes().Count)
            {
                sum += num2.getBytes()[i];
            }
            resultBytes.Add((byte)sum);
            tmp = sum >> 8;
        }
        return new BigInteger(resultBytes);

    }
    public BigInteger SUB(BigInteger num1, BigInteger num2)
    {
        List<byte> resultBytes = new List<byte>();
        int maxLength = Math.Max(num1.getBytes().Count, num2.getBytes().Count);
        int tmp = 0;
        for (int i = 0; i < maxLength; i++)
        {
            int diff = tmp;

            if (i < num1.getBytes().Count)
            {
                diff += num1.getByte(i);
            }

            if (i < num2.getBytes().Count)
            {
                diff -= num2.getByte(i);
            }

            if (diff < 0)
            {
                diff += 256;
                tmp = -1;
            }
            else
            {
                tmp = 0;
            }

            resultBytes.Add((byte)diff);
        }
        while (resultBytes.Count > 0 && resultBytes[resultBytes.Count - 1] == 0)
        {
            resultBytes.RemoveAt(resultBytes.Count - 1);
        }
        return new BigInteger (resultBytes);
    }
    /*public BigInteger MOD(BigInteger number, BigInteger modul)
    {
        List<byte> resultBytes = new List<byte>();
        BigInteger tmp = new BigInteger();
        for (int i = number.getBytes().Count - 1; i >= 0; i--)
        {
            tmp = (tmp << 8) | number.getBytes()[i];
            if (tmp >= modul)
            {
                BigInteger quotient = remainder / modul;
                tmp -= quotient * modul;
            }
        }

        while (tmp > 0)
        {
            byte nextByte = (byte)(tmp & 0xFF);
            resultBytes.Insert(0, nextByte);
            tmp >>= 8;
        }
        return new BigInteger(resultBytes);
    }*/
}

public class Program
{
    static void Main() {
        BigInteger bigInteger1 = new BigInteger(); 
        BigInteger bigInteger2 = new BigInteger(); 
        BigInteger bigInteger3 = new BigInteger();
        BigInteger method = new BigInteger();
        bigInteger1.setHex("ff");
        Console.WriteLine();
        bigInteger2.setHex("22e962951cb6cd2ce279ab0e2095825c141d48ef3ca9dabf253e38760b57fe03");
        Console.WriteLine();
        Console.WriteLine("HEX:");
        Console.WriteLine(bigInteger1.getHex());
        Console.WriteLine();
        Console.WriteLine("XOR:");
        Console.WriteLine(method.XOR(bigInteger1, bigInteger2).getHex());
        Console.WriteLine();
        Console.WriteLine("Inversion of first number:");
        Console.WriteLine(method.INV(bigInteger1).getHex());
        Console.WriteLine();
        Console.WriteLine("Inversion of second number:");
        Console.WriteLine(method.INV(bigInteger2).getHex());
        Console.WriteLine();
        Console.WriteLine("OR:");
        Console.WriteLine(method.OR(bigInteger1, bigInteger2).getHex());
        Console.WriteLine();
        Console.WriteLine("AND:");
        Console.WriteLine(method.AND(bigInteger1, bigInteger2).getHex());
        Console.WriteLine();
        Console.WriteLine("ShiftR");
        Console.WriteLine(method.ShiftRight(bigInteger1, 6).getHex());
        Console.WriteLine();
        Console.WriteLine("ShiftL");
        Console.WriteLine(method.ShiftLeft(bigInteger1, 6).getHex());
        Console.WriteLine();
        Console.WriteLine("ADD:");
        Console.WriteLine(method.Add(bigInteger1, bigInteger2).getHex());
        Console.WriteLine();
        Console.WriteLine("SUB:");
        Console.WriteLine(method.SUB(bigInteger1, bigInteger2).getHex());
        Console.WriteLine();
    }
}
