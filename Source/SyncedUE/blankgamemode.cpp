// Fill out your copyright notice in the Description page of Project Settings.

#include "SyncedUE.h"
#include "blankgamemode.h"
#include "nothing.h"


Ablankgamemode::Ablankgamemode(const FObjectInitializer &ObjectInitializer) : Super(ObjectInitializer)
{
	DefaultPawnClass = Anothing::StaticClass();
}

