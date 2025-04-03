using System.Reflection;

namespace MockingDependencies;

using NSubstitute;
using NSubstitute.ExceptionExtensions;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void AdvanceAddition_ValidState_StatusCode200()
    {
        var dependency1 = new ExternalDependencyImplementation();
        
        var sut = new MathService1(dependency1);

        var result = sut.AdvanceAddition(1, 2);

        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(3, result.Data);
    }

    [TestMethod]
    public void AdvanceAddition_InternalServerError_StatusCode500_ByOverride()
    {
        var dependency1 = new ExternalDepenedencyOverrideImplementation
        {
            ThrowException = true,
        };

        var sut = new MathService1(dependency1);
        
        var result = sut.AdvanceAddition(1, 2);

        Assert.AreEqual(500, result.StatusCode);
        Assert.AreEqual(null, result.Data);
    }
    
    // [TestMethod]
    // public void AdvanceAddition_InternalServerError_StatusCode500_ByHiding()
    // {
    //     var dependency1 = new ExternalDepenedencyHidingImplementation
    //     {
    //         ThrowException = true,
    //     };
    //
    //     var sut = new MathService1(dependency1);
    //     
    //     var result = sut.AdvanceAddition(1, 2);
    //
    //     Assert.AreEqual(500, result.StatusCode);
    //     Assert.AreEqual(null, result.Data);
    // }
    
    [TestMethod]
    public void AdvanceAddition_InternalServerError_StatusCode500_BySubstitute()
    {
        var dependency1 = Substitute.For<ExternalDependencyImplementation>();
        dependency1.DoStuff(Arg.Any<int>(), Arg.Any<int>())
            .Throws(new Exception());
        
        var sut = new MathService1(dependency1);
        
        var result = sut.AdvanceAddition(1, 2);

        Assert.AreEqual(500, result.StatusCode);
        Assert.AreEqual(null, result.Data);
    }
    
    public interface IExernalDependency1
    {
        int DoStuff(int x, int y);
    }

    public class MathService1
    {
        private readonly ExternalDependencyImplementation _dependency1;
        
        public MathService1(ExternalDependencyImplementation dependency1)
        {
            _dependency1 = dependency1;
        }

        public Response AdvanceAddition(int x, int y)
        {
            try
            {
                var result = _dependency1.DoStuff(x, y);
                return new Response
                {
                    StatusCode = 200,
                    Data = result,
                };
            }
            catch
            {
                return new Response
                {
                    StatusCode = 500,
                    Data = null,
                };
            }
        }
    }

    public class Response
    {
        public int StatusCode { get; set; }
        public int? Data { get; set; }
    }

    public class ExternalDependencyImplementation
    {
        public virtual int DoStuff(int x, int y)
        {
            return x + y;
        }
    }

    public class ExternalDepenedencyOverrideImplementation : ExternalDependencyImplementation
    {
        public bool ThrowException { get; set; } = false;
        public override int DoStuff(int x, int y)
        {
            if (ThrowException)
            {
                throw new Exception();
            }

            return x + y;
        }
    }
    
    public class ExternalDepenedencyHidingImplementation : IExernalDependency1
    {
        public bool ThrowException { get; set; } = false;
        public new int DoStuff(int x, int y)
        {
            if (ThrowException)
            {
                throw new Exception();
            }

            return x + y;
        }
    }
}
