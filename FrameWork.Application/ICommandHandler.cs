using FrameWork.Application.Behaviours;


namespace FrameWork.Application
{

	public interface ICommandHandler
	{

	}

	public interface ICommandHandler<T> : ICommandHandler where T : ICommand
	{
		void Handle(T command);
	}

}
