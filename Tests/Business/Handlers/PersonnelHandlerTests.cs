
using Business.Handlers.Personnels.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Personnels.Queries.GetPersonnelQuery;
using Entities.Concrete;
using static Business.Handlers.Personnels.Queries.GetPersonnelsQuery;
using static Business.Handlers.Personnels.Commands.CreatePersonnelCommand;
using Business.Handlers.Personnels.Commands;
using Business.Constants;
using static Business.Handlers.Personnels.Commands.UpdatePersonnelCommand;
using static Business.Handlers.Personnels.Commands.DeletePersonnelCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
	[TestFixture]
	public class PersonnelHandlerTests
	{
		Mock<IPersonnelRepository> _personnelRepository;
		Mock<IMediator> _mediator;
		[SetUp]
		public void Setup()
		{
			_personnelRepository = new Mock<IPersonnelRepository>();
			_mediator = new Mock<IMediator>();
		}

		[Test]
		public async Task Personnel_GetQuery_Success()
		{
			//Arrange
			var query = new GetPersonnelQuery();

			_personnelRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Personnel, bool>>>())).ReturnsAsync(new Personnel()
//propertyler buraya yazılacak
//{																		
//PersonnelId = 1,
//PersonnelName = "Test"
//}
);

			var handler = new GetPersonnelQueryHandler(_personnelRepository.Object, _mediator.Object);

			//Act
			var x = await handler.Handle(query, new System.Threading.CancellationToken());

			//Asset
			x.Success.Should().BeTrue();
			//x.Data.PersonnelId.Should().Be(1);

		}

		[Test]
		public async Task Personnel_GetQueries_Success()
		{
			//Arrange
			var query = new GetPersonnelsQuery();

			_personnelRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Personnel, bool>>>()))
						.ReturnsAsync(new List<Personnel> { new Personnel() { /*TODO:propertyler buraya yazılacak PersonnelId = 1, PersonnelName = "test"*/ } });

			var handler = new GetPersonnelsQueryHandler(_personnelRepository.Object, _mediator.Object);

			//Act
			var x = await handler.Handle(query, new System.Threading.CancellationToken());

			//Asset
			x.Success.Should().BeTrue();
			((List<Personnel>)x.Data).Count.Should().BeGreaterThan(1);

		}

		[Test]
		public async Task Personnel_CreateCommand_Success()
		{
			Personnel rt = null;
			//Arrange
			var command = new CreatePersonnelCommand();
			//propertyler buraya yazılacak
			//command.PersonnelName = "deneme";

			_personnelRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Personnel, bool>>>()))
						.ReturnsAsync(rt);

			_personnelRepository.Setup(x => x.Add(It.IsAny<Personnel>())).Returns(new Personnel());

			var handler = new CreatePersonnelCommandHandler(_personnelRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			x.Success.Should().BeTrue();
			x.Message.Should().Be(Messages.Added);
		}

		[Test]
		public async Task Personnel_CreateCommand_NameAlreadyExist()
		{
			//Arrange
			var command = new CreatePersonnelCommand();
			//propertyler buraya yazılacak 
			//command.PersonnelName = "test";

			_personnelRepository.Setup(x => x.Query())
										   .Returns(new List<Personnel> { new Personnel() { /*TODO:propertyler buraya yazılacak PersonnelId = 1, PersonnelName = "test"*/ } }.AsQueryable());

			_personnelRepository.Setup(x => x.Add(It.IsAny<Personnel>())).Returns(new Personnel());

			var handler = new CreatePersonnelCommandHandler(_personnelRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			x.Success.Should().BeFalse();
			x.Message.Should().Be(Messages.NameAlreadyExist);
		}

		[Test]
		public async Task Personnel_UpdateCommand_Success()
		{
			//Arrange
			var command = new UpdatePersonnelCommand();
			//command.PersonnelName = "test";

			_personnelRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Personnel, bool>>>()))
						.ReturnsAsync(new Personnel() { /*TODO:propertyler buraya yazılacak PersonnelId = 1, PersonnelName = "deneme"*/ });

			_personnelRepository.Setup(x => x.Update(It.IsAny<Personnel>())).Returns(new Personnel());

			var handler = new UpdatePersonnelCommandHandler(_personnelRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			x.Success.Should().BeTrue();
			x.Message.Should().Be(Messages.Updated);
		}

		[Test]
		public async Task Personnel_DeleteCommand_Success()
		{
			//Arrange
			var command = new DeletePersonnelCommand();

			_personnelRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Personnel, bool>>>()))
						.ReturnsAsync(new Personnel() { /*TODO:propertyler buraya yazılacak PersonnelId = 1, PersonnelName = "deneme"*/});

			_personnelRepository.Setup(x => x.Delete(It.IsAny<Personnel>()));

			var handler = new DeletePersonnelCommandHandler(_personnelRepository.Object, _mediator.Object);
			var x = await handler.Handle(command, new System.Threading.CancellationToken());

			x.Success.Should().BeTrue();
			x.Message.Should().Be(Messages.Deleted);
		}
	}
}

